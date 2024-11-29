using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS8321 // Local function is declared but never used
#pragma warning disable S1172
#pragma warning disable S6608
#pragma warning disable S125

// Step up out of execution path : {solutionPath}/tools/SchemaToCSharp/bin/Debug/net8.0
const string solutionPath = "../../../../../";
const string outputPath = $"{solutionPath}src/Contracts/";
const string inputPath = $"{solutionPath}tools/SchemaToCSharp/cdms-public-openapi-v0.1.json";

Directory.GetFiles(outputPath, "*.cs").ToList().ForEach(File.Delete);

var stream = new FileStream(inputPath, FileMode.Open);
var openApiDocument = new OpenApiStreamReader().Read(stream, out _);

var namespaceDeclaration = FileScopedNamespaceDeclaration(ParseName("Defra.PhaImportNotifications.Contracts"));

foreach (var (schemaName, schema) in openApiDocument.Components.Schemas)
{
    var syntax = schema.Type switch
    {
        "integer" => CreateEnumSyntax(),
        "object" => CreateClassSyntax(),
        _ => throw new ArgumentOutOfRangeException(schema.Type, "Unknown schema type"),
    };

    await using var streamWriter = new StreamWriter($"{outputPath}/{schemaName}.g.cs", false);
    syntax.NormalizeWhitespace().WithTrailingTrivia(ElasticCarriageReturnLineFeed).WriteTo(streamWriter);
    continue;

    SyntaxNode CreateEnumSyntax()
    {
        var values = schema.Enum.Select(v => CreateEnumValue((v as OpenApiString)!.Value));
        var @enum = CreateEnum(schemaName).AddMembers(values.ToArray());

        return namespaceDeclaration.AddMembers(@enum);
    }

    SyntaxNode CreateClassSyntax()
    {
        var properties = schema
            .Properties.Where(p => !p.Key.StartsWith('_'))
            .Select(p => CreateProperty(p.Key, p.Value));

        var @class = CreateClass(schemaName).AddMembers(properties.ToArray<MemberDeclarationSyntax>());

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
    var typeName = schema.Type switch
    {
        "string" => schema.Format == "date-time" ? "DateTime" : "string",
        "integer" => RefTypeName(schema, "int"),
        "number" => "decimal",
        "boolean" => "bool",
        "object" => RefTypeName(schema, schema.Type),
        "array" => RefTypeName(schema.Items, schema.Items.Type),
        _ => "object",
    };

    return ParseTypeName(schema.Type == "array" ? $"List<{typeName}>" : typeName);
}

static string RefTypeName(OpenApiSchema schema, string defaultTypeName) =>
    schema.Reference?.ReferenceV3?.Split("/").Last() ?? defaultTypeName;

static EnumMemberDeclarationSyntax CreateEnumValue(string name) => EnumMemberDeclaration(name);

static PropertyDeclarationSyntax CreateProperty(string name, OpenApiSchema schema)
{
    var typeSyntax = CreatePropertyType(schema);
    var modifiers = new List<SyntaxToken> { Token(SyntaxKind.PublicKeyword) };

    if (schema.Nullable)
    {
        typeSyntax = NullableType(typeSyntax);
    }
    else
    {
        modifiers.Add(Token(SyntaxKind.RequiredKeyword));
    }

    var attributes = new List<AttributeListSyntax> { CreateSimpleAttributeList("JsonPropertyName", name) };

    var description = schema.Description;
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
    ClassDeclaration(Identifier(name)).AddModifiers(Token(SyntaxKind.PublicKeyword));

static UsingDirectiveSyntax CreateUsing(string fqn) => UsingDirective(ParseName(fqn));

static EnumDeclarationSyntax CreateEnum(string name) =>
    EnumDeclaration(name).AddModifiers(Token(SyntaxKind.PublicKeyword));

static AttributeListSyntax CreateSimpleAttributeList(string type, string arg1) =>
    AttributeList(SingletonSeparatedList(CreateSimpleAttribute(type, arg1)));

static AttributeSyntax CreateSimpleAttribute(string type, string arg1) =>
    Attribute(ParseName(type)).WithArgumentList(ParseAttributeArgumentList($"(\"{arg1}\")"));

static string CreatePropertyName(string s) => char.ToUpper(s[0]) + s[1..];
