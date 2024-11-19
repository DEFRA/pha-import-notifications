using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

#pragma warning disable CS8321 // Local function is declared but never used
#pragma warning disable S1172
#pragma warning disable S6608

// Stop up out of execution path : {solutionPath}/tools/SchemaToCSharp/bin/Debug/net8.0
const string solutionPath = "../../../../../";
const string outputPath = $"{solutionPath}src/Contracts/";
const string inputPath = $"{solutionPath}tools/SchemaToCSharp/cdms-public-openapi-v0.1.json";

var stream = new FileStream(inputPath, FileMode.Open);
var namespaceDeclaration = NamespaceDeclaration(ParseName("Defra.PhaImportNotifications.Contracts"));

var openApiDocument = new OpenApiStreamReader().Read(stream, out var diagnostic);

diagnostic.Errors.ToList().ForEach(e => Console.WriteLine(e.Message));

var objects = openApiDocument.Components.Schemas.Where(s => s.Value.Type == "object").ToList();

Directory.GetFiles(outputPath, "*.g.cs").ToList().ForEach(File.Delete);

foreach (var (schemaName, schema) in objects)
{
    var properties = schema.Properties.Select(p => CreatePropertyFrom(p.Key, p.Value));
    var @class = CreateClass(schemaName).AddMembers(properties.ToArray<MemberDeclarationSyntax>());

    var ns = namespaceDeclaration
        .AddUsings(CreateUsing("System.Text.Json.Serialization"), CreateUsing("System.ComponentModel"))
        .AddMembers(@class);

    await using var streamWriter = new StreamWriter($"{outputPath}/{schemaName}.g.cs", false);

    ns.NormalizeWhitespace().WithTrailingTrivia(ElasticCarriageReturnLineFeed).WriteTo(streamWriter);
}

Console.WriteLine("Done");

return;

static TypeSyntax CreatePropertyType(OpenApiSchema schema) =>
    schema.Type switch
    {
        "string" => schema.Format == "date-time" ? ParseTypeName("DateTime") : ParseTypeName("string"),
        "integer" => ParseTypeName("int"),
        "number" => ParseTypeName("decimal"),
        "boolean" => ParseTypeName("bool"),
        "object" => CreateObjectPropertyType(schema),
        "array" => CreateArrayPropertyType(schema),
        _ => ParseTypeName("object"),
    };

static TypeSyntax CreateObjectPropertyType(OpenApiSchema schema) =>
    ParseTypeName(schema.Reference?.ReferenceV3?.Split("/").Last() ?? "object");

static TypeSyntax CreateArrayPropertyType(OpenApiSchema schema)
{
    return ParseTypeName("Array");
}

static PropertyDeclarationSyntax CreatePropertyFrom(string name, OpenApiSchema schema) =>
    CreateProperty(name, CreatePropertyType(schema), schema.Description);

static PropertyDeclarationSyntax CreateProperty(string name, TypeSyntax typeSyntax, string description)
{
    var attributes = new List<AttributeListSyntax> { CreateSimpleAttributeList("JsonPropertyName", name) };

    if (!string.IsNullOrEmpty(description))
        attributes.Add(CreateSimpleAttributeList("Description", description));

    return PropertyDeclaration(typeSyntax, CapitalizeFirstLetter(name))
        .AddModifiers(Token(SyntaxKind.PublicKeyword))
        .AddAttributeLists(attributes.ToArray())
        .AddAccessorListAccessors(CreateGetterAndSetter());
}

static AccessorDeclarationSyntax[] CreateGetterAndSetter() =>
    [
        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
        AccessorDeclaration(SyntaxKind.InitAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
    ];

static ClassDeclarationSyntax CreateClass(string name) =>
    ClassDeclaration(Identifier(name)).AddModifiers(Token(SyntaxKind.PublicKeyword));

static UsingDirectiveSyntax CreateUsing(string fqn) => UsingDirective(ParseName(fqn));

static ClassDeclarationSyntax CreateEnum(string name) =>
    ClassDeclaration(Identifier(name)).AddModifiers(Token(SyntaxKind.PublicKeyword));

static AttributeListSyntax CreateSimpleAttributeList(string type, string arg1) =>
    AttributeList(SingletonSeparatedList(CreateSimpleAttribute(type, arg1)));

static AttributeSyntax CreateSimpleAttribute(string type, string arg1) =>
    Attribute(ParseName(type)).WithArgumentList(ParseAttributeArgumentList($"(\"{arg1}\")"));

static string CapitalizeFirstLetter(string s)
{
    if (string.IsNullOrEmpty(s))
        return s;
    if (s.Length == 1)
        return s.ToUpper();
    return string.Concat(s.Remove(1).ToUpper(), s.AsSpan(1));
}
