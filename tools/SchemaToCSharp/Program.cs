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
const string inputPath = $"{solutionPath}tools/SchemaToCSharp/openapi-data-api.json";
const string outputNamespace = "Defra.PhaImportNotifications.Contracts";

Directory.GetFiles(outputPath, "*.g.cs").ToList().ForEach(File.Delete);

var stream = new FileStream(inputPath, FileMode.Open);
var openApiDocument = new OpenApiStreamReader().Read(stream, out _);
var namespaceDeclaration = FileScopedNamespaceDeclaration(ParseName(outputNamespace));

foreach (var (key, componentsSchema) in openApiDocument.Components.Schemas)
{
    componentsSchema.Title = CreateTypeName(key.Split(".").AsEnumerable().Last());
}

foreach (var (_, componentsSchema) in openApiDocument.Components.Schemas)
{
    var typeName = componentsSchema.Title;

    var syntax = componentsSchema.Type switch
    {
        OpenApiTypes.String => null,
        OpenApiTypes.Integer => throw new ArgumentOutOfRangeException(
            componentsSchema.Type,
            "Unsupported schema type int"
        ), //CreateEnumSyntax(componentsSchema, typeName),
        OpenApiTypes.Object => CreateTypeSyntax(componentsSchema, typeName),
        _ => throw new ArgumentOutOfRangeException(componentsSchema.Type, "Unknown schema type"),
    };

    if (syntax is not null)
    {
        await using var streamWriter = new StreamWriter($"{outputPath}/{typeName}.g.cs", false);
        syntax.NormalizeWhitespace().WithTrailingTrivia(ElasticCarriageReturnLineFeed).WriteTo(streamWriter);
    }
}

return;

#pragma warning disable CS8321 // Local function is declared but never used
SyntaxNode CreateEnumSyntax(OpenApiSchema schema, string enumName)
#pragma warning restore CS8321 // Local function is declared but never used
{
    var values = schema.Enum.Select(v => CreateEnumValue(enumName, (v as OpenApiString)!.Value));
    var @enum = CreateEnum(enumName).AddMembers(values.Where(x => x != null).Select(x => x!).ToArray());

    return namespaceDeclaration.AddMembers(@enum);
}

SyntaxNode CreateTypeSyntax(OpenApiSchema schema, string typeName)
{
    var properties = schema.Properties.Select(p => CreateProperty(typeName, p.Key, p.Value));

    var @class = CreateRecord(typeName)
        .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
        .AddMembers(properties.ToArray<MemberDeclarationSyntax>())
        .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

    return CompilationUnit()
        .AddUsings(CreateUsing("System.Text.Json.Serialization"), CreateUsing("System.ComponentModel"))
        .WithLeadingTrivia(Trivia(NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), true)))
        .AddMembers(namespaceDeclaration)
        .AddMembers(@class);
}

static TypeSyntax CreatePropertyType(OpenApiSchema propertySchema)
{
    if (propertySchema.AllOf.Any())
    {
        return CreatePropertyType(propertySchema.AllOf[0]);
    }

    var typeName = propertySchema.Type switch
    {
        OpenApiTypes.String => CreateStringReferenceTypeName(propertySchema),
        OpenApiTypes.Integer => DotNetTypes.Int, //CreateReferenceTypeName(schema, DotNetTypes.Int),
        OpenApiTypes.Number => DotNetTypes.Decimal,
        OpenApiTypes.Boolean => DotNetTypes.Bool,
        OpenApiTypes.Object => propertySchema.Title ?? propertySchema.Type,
        OpenApiTypes.Array => $"List<{propertySchema.Items.Title ?? propertySchema.Items.Type}>",
        _ => DotNetTypes.Object,
    };

    return ParseTypeName(typeName);
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

static EnumMemberDeclarationSyntax? CreateEnumValue(string schemaName, string name)
{
    var ignored = Ignored.Properties.TryGetValue(schemaName, out var properties) && properties.Contains(name);

    return ignored ? null : EnumMemberDeclaration(name);
}

static PropertyDeclarationSyntax CreateProperty(string schemaName, string propertyName, OpenApiSchema propertySchema)
{
    var typeSyntax = CreatePropertyType(propertySchema);
    var modifiers = new List<SyntaxToken> { Token(SyntaxKind.PublicKeyword) };

    var propertyIgnored =
        Ignored.Properties.TryGetValue(schemaName, out var properties) && properties.Contains(propertyName);

    if (propertySchema.Nullable || propertyIgnored)
    {
        typeSyntax = NullableType(typeSyntax);
    }
    else
    {
        modifiers.Add(Token(SyntaxKind.RequiredKeyword));
    }

    var attributes = new List<AttributeListSyntax> { CreateSimpleAttributeList("JsonPropertyName", propertyName) };

    if (propertySchema.Enum?.Any() == true)
    {
        attributes.AddRange(CreatePropertyEnumExampleValueAttributes(schemaName, propertyName, propertySchema));
    }

    if (propertyIgnored)
        attributes.Add(AttributeList(SingletonSeparatedList(Attribute(ParseName("JsonIgnore")))));

    if (
        !(
            Meta.Descriptions.TryGetValue(schemaName, out var descriptions)
            && descriptions.TryGetValue(propertyName, out var description)
        )
    )
    {
        description = propertySchema.Description;
    }

    if (!string.IsNullOrEmpty(description))
        attributes.Add(CreateSimpleAttributeList("Description", description));

    return PropertyDeclaration(typeSyntax, CreatePropertyName(propertyName))
        .AddModifiers(modifiers.ToArray())
        .AddAttributeLists(attributes.ToArray())
        .AddAccessorListAccessors(
            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
            AccessorDeclaration(SyntaxKind.InitAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );
}

static RecordDeclarationSyntax CreateRecord(string name) =>
    RecordDeclaration(Token(SyntaxKind.RecordKeyword), Identifier(name))
        .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword));

static UsingDirectiveSyntax CreateUsing(string fqn) => UsingDirective(ParseName(fqn));

static EnumDeclarationSyntax CreateEnum(string name) =>
    EnumDeclaration(name).AddModifiers(Token(SyntaxKind.PublicKeyword));

static List<AttributeListSyntax> CreatePropertyEnumExampleValueAttributes(
    string typeName,
    string propertyName,
    OpenApiSchema schema
)
{
    Ignored.Properties.TryGetValue($"{typeName}.{propertyName}", out var ignoredProperties);
    bool ValueIgnored(string value) => ignoredProperties != null && ignoredProperties.Contains(value);

    var enumValues = schema.Enum.OfType<OpenApiString>();
    return CreateEnumExampleValueAttributes(enumValues.Where(e => !ValueIgnored(e.Value)).ToList());
}

static List<AttributeListSyntax> CreateEnumExampleValueAttributes(IList<OpenApiString> enumValues) =>
    enumValues.Select(v => CreateSimpleAttributeList("ExampleValue", v.Value)).ToList();

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
