using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using SchemaToCSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS8321 // Local function is declared but never used
#pragma warning disable S1172
#pragma warning disable S6608
#pragma warning disable S125

const string solutionPath = "../../../../../";
const string outputPath = $"{solutionPath}src/Contracts/";
const string inputPath = $"{solutionPath}tools/SchemaToCSharp/openapi.json";

Directory.GetFiles(outputPath, "*.g.cs").ToList().ForEach(File.Delete);

var stream = new FileStream(inputPath, FileMode.Open);
var openApiDocument = new OpenApiStreamReader().Read(stream, out _);

var namespaceDeclaration = FileScopedNamespaceDeclaration(ParseName("Defra.PhaImportNotifications.Contracts"));

foreach (var (schemaName, schema) in openApiDocument.Components.Schemas)
{
    var syntax = schema.Type switch
    {
        "integer" => CreateEnumSyntax(),
        "string" => CreateEnumSyntax(),
        "object" => CreateTypeSyntax(),
        _ => throw new ArgumentOutOfRangeException(schema.Type, "Unknown schema type"),
    };

    await using var streamWriter = new StreamWriter($"{outputPath}/{CreateTypeName(schemaName)}.g.cs", false);
    syntax.NormalizeWhitespace().WithTrailingTrivia(ElasticCarriageReturnLineFeed).WriteTo(streamWriter);
    continue;

    SyntaxNode CreateEnumSyntax()
    {
        var values = schema.Enum.Select(v => CreateEnumValue(schemaName, (v as OpenApiString)!.Value));
        var @enum = CreateEnum(schemaName).AddMembers(values.Where(x => x != null).Select(x => x!).ToArray());

        return namespaceDeclaration.AddMembers(@enum);
    }

    SyntaxNode CreateTypeSyntax()
    {
        var properties = schema.Properties.Select(p => CreateProperty(schemaName, p.Key, p.Value));
        var @class = CreateRecord(schemaName)
            .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
            .AddMembers(properties.ToArray<MemberDeclarationSyntax>())
            .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

        return CompilationUnit()
            .AddUsings(CreateUsing("System.Text.Json.Serialization"), CreateUsing("System.ComponentModel"))
            .WithLeadingTrivia(Trivia(NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), true)))
            .AddMembers(namespaceDeclaration)
            .AddMembers(@class);
    }
}

return;

static TypeSyntax CreatePropertyType(OpenApiSchema schema)
{
    if (schema.AllOf.Any())
    {
        return CreatePropertyType(schema.AllOf.First());
    }

    var typeName = schema.Type switch
    {
        "string" => CreateStringReferenceTypeName(schema),
        "integer" => CreateReferenceTypeName(schema, "int"),
        "number" => "decimal",
        "boolean" => "bool",
        "object" => CreateReferenceTypeName(schema, schema.Type),
        "array" => CreateReferenceTypeName(schema.Items, schema.Items.Type),
        _ => "object",
    };

    typeName = CreateTypeName(typeName);

    return ParseTypeName(schema.Type == "array" ? $"List<{typeName}>" : typeName);
}

static string CreateStringReferenceTypeName(OpenApiSchema schema)
{
    if (schema.Enum.Any())
    {
        return CreateReferenceTypeName(schema, "string");
    }

    return schema.Format == "date-time" ? "DateTime" : "string";
}

static string CreateReferenceTypeName(OpenApiSchema schema, string defaultTypeName) =>
    schema.Reference?.ReferenceV3?.Split("/").Last() ?? defaultTypeName;

static EnumMemberDeclarationSyntax? CreateEnumValue(string schemaName, string name)
{
    var ignored = Ignored.Properties.TryGetValue(schemaName, out var properties) && properties.Contains(name);

    return ignored ? null : EnumMemberDeclaration(name);
}

static PropertyDeclarationSyntax CreateProperty(string schemaName, string name, OpenApiSchema schema)
{
    var typeSyntax = CreatePropertyType(schema);
    var modifiers = new List<SyntaxToken> { Token(SyntaxKind.PublicKeyword) };
    var ignored = Ignored.Properties.TryGetValue(schemaName, out var properties) && properties.Contains(name);

    if (schema.Nullable || ignored)
    {
        typeSyntax = NullableType(typeSyntax);
    }
    else
    {
        modifiers.Add(Token(SyntaxKind.RequiredKeyword));
    }

    var attributes = new List<AttributeListSyntax> { CreateSimpleAttributeList("JsonPropertyName", name) };

    if (ignored)
        attributes.Add(AttributeList(SingletonSeparatedList(Attribute(ParseName("JsonIgnore")))));

    if (
        !(
            Meta.Descriptions.TryGetValue(schemaName, out var descriptions)
            && descriptions.TryGetValue(name, out var description)
        )
    )
    {
        description = schema.Description;
    }

    if (!string.IsNullOrEmpty(description))
        attributes.Add(CreateSimpleAttributeList("Description", description));

    return PropertyDeclaration(typeSyntax, CreatePropertyName(name))
        .AddModifiers(modifiers.ToArray())
        .AddAttributeLists(attributes.ToArray())
        .AddAccessorListAccessors(
            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
            AccessorDeclaration(SyntaxKind.InitAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
}

static ClassDeclarationSyntax CreateClass(string name) =>
    ClassDeclaration(Identifier(CreateTypeName(name)))
        .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword));

static RecordDeclarationSyntax CreateRecord(string name) =>
    RecordDeclaration(Token(SyntaxKind.RecordKeyword), Identifier(CreateTypeName(name)))
        .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword));

static UsingDirectiveSyntax CreateUsing(string fqn) => UsingDirective(ParseName(fqn));

static EnumDeclarationSyntax CreateEnum(string name) =>
    EnumDeclaration(name).AddModifiers(Token(SyntaxKind.PublicKeyword));

static AttributeListSyntax CreateSimpleAttributeList(string type, string arg1) =>
    AttributeList(SingletonSeparatedList(CreateSimpleAttribute(type, arg1)));

static AttributeSyntax CreateSimpleAttribute(string type, string arg1) =>
    Attribute(ParseName(type)).WithArgumentList(ParseAttributeArgumentList($"(\"{arg1}\")"));

static string CreatePropertyName(string s) => char.ToUpper(s[0]) + s[1..];

static string CreateTypeName(string name)
{
    var identifier = name;
    if (Rename.Types.TryGetValue(name, out var typeName))
        identifier = typeName;

    return identifier;
}
