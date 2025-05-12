using System.Data;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using SchemaToCSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

const string solutionPath = "../../../../../";
const string outputPath = $"{solutionPath}src/Contracts/";
const string inputPath = $"{solutionPath}tools/SchemaToCSharp/openapi.json";
const string outputNamespace = "Defra.PhaImportNotifications.Contracts";

Directory.GetFiles(outputPath, "*.g.cs").ToList().ForEach(File.Delete);

var stream = new FileStream(inputPath, FileMode.Open);
var openApiDocument = new OpenApiStreamReader().Read(stream, out _);
var namespaceDeclaration = FileScopedNamespaceDeclaration(ParseName(outputNamespace));

var schemas = openApiDocument.Components.Schemas.Select(d => d.Value);
foreach (var schema in schemas)
{
    var syntax = schema.Type switch
    {
        OpenApiTypes.String => null,
        OpenApiTypes.Integer => CreateEnumSyntax(schema),
        OpenApiTypes.Object => CreateTypeSyntax(schema),
        _ => throw new ArgumentOutOfRangeException(schema.Type, "Unknown schema type"),
    };

    if (syntax is not null)
    {
        await using var streamWriter = new StreamWriter($"{outputPath}/{CreateTypeName(schema.Title)}.g.cs", false);
        syntax.NormalizeWhitespace().WithTrailingTrivia(ElasticCarriageReturnLineFeed).WriteTo(streamWriter);
    }
}

return;

SyntaxNode CreateEnumSyntax(OpenApiSchema schema)
{
    var values = schema.Enum.Select(v => CreateEnumValue(schema.Title, (v as OpenApiString)!.Value));
    var @enum = CreateEnum(schema.Title).AddMembers(values.Where(x => x != null).Select(x => x!).ToArray());

    return namespaceDeclaration.AddMembers(@enum);
}

SyntaxNode CreateTypeSyntax(OpenApiSchema schema)
{
    var properties = schema.Properties.Select(p => CreateProperty(schema.Title, p.Key, p.Value));
    var @class = CreateRecord(schema.Title)
        .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
        .AddMembers(properties.ToArray<MemberDeclarationSyntax>())
        .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

    return CompilationUnit()
        .AddUsings(CreateUsing("System.Text.Json.Serialization"), CreateUsing("System.ComponentModel"))
        .WithLeadingTrivia(Trivia(NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), true)))
        .AddMembers(namespaceDeclaration)
        .AddMembers(@class);
}

static TypeSyntax CreatePropertyType(OpenApiSchema schema)
{
    if (schema.AllOf.Any())
    {
        return CreatePropertyType(schema.AllOf[0]);
    }

    var typeName = schema.Type switch
    {
        OpenApiTypes.String => CreateStringReferenceTypeName(schema),
        OpenApiTypes.Integer => CreateReferenceTypeName(schema, DotNetTypes.Int),
        OpenApiTypes.Number => DotNetTypes.Decimal,
        OpenApiTypes.Boolean => DotNetTypes.Bool,
        OpenApiTypes.Object => CreateReferenceTypeName(schema, schema.Type),
        OpenApiTypes.Array => CreateReferenceTypeName(schema.Items, schema.Items.Type),
        _ => DotNetTypes.Object,
    };

    typeName = CreateTypeName(typeName);

    return ParseTypeName(schema.Type == OpenApiTypes.Array ? $"List<{typeName}>" : typeName);
}

static string CreateStringReferenceTypeName(OpenApiSchema schema)
{
    return schema.Format switch
    {
        OpenApiFormats.DateTime => DotNetTypes.DateTime,
        OpenApiFormats.Date => DotNetTypes.DateOnly,
        _ => DotNetTypes.String,
    };
}

static string CreateReferenceTypeName(OpenApiSchema schema, string defaultTypeName) =>
    // Special case FinalState for now because it is the only integer enum
    (schema.Title is not null && schema.Enum.Count == 0)
    || schema.Title == "FinalState"
        ? schema.Title
        : defaultTypeName;

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

    AddEnumExampleValueAttributes(schema, attributes);

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

static RecordDeclarationSyntax CreateRecord(string name) =>
    RecordDeclaration(Token(SyntaxKind.RecordKeyword), Identifier(CreateTypeName(name)))
        .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword));

static UsingDirectiveSyntax CreateUsing(string fqn) => UsingDirective(ParseName(fqn));

static EnumDeclarationSyntax CreateEnum(string name) =>
    EnumDeclaration(name).AddModifiers(Token(SyntaxKind.PublicKeyword));

static void AddEnumExampleValueAttributes(OpenApiSchema schema, List<AttributeListSyntax> attributes)
{
    if (schema.AllOf?.FirstOrDefault()?.Enum.Any() == true)
    {
        var enumSchema = schema.AllOf[0];
        Ignored.Properties.TryGetValue(enumSchema.Title, out var ignoredProperties);
        var enumValues = enumSchema
            .Enum.OfType<OpenApiString>()
            .Select(s => s.Value)
            .Where(v => ignoredProperties?.Contains(v) != true);
        attributes.AddRange(enumValues.Select(v => CreateSimpleAttributeList("ExampleValue", v)));
    }
}

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
