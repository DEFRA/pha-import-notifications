using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Defra.PhaImportNotifications.Api.OpenApi;

/// <summary>
/// Taken from https://github.com/martincostello/aspnetcore-openapi/blob/main/src/TodoApp/OpenApi/AspNetCore/AddSchemaDescriptionsTransformer.cs
/// This version includes fix for nested type names and can be used for types from different assemblies.
/// </summary>
public class XmlDocsSchemaTransformer<T> : IOpenApiSchemaTransformer
{
    private static readonly Assembly s_thisAssembly = typeof(T).Assembly;
    private readonly ConcurrentDictionary<string, string?> _descriptions = [];
    private XPathNavigator? _navigator;

    public Task TransformAsync(
        OpenApiSchema schema,
        OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken
    )
    {
        // Assign a description from the XML documentation from either the type or the property associated with the schema
        if (
            schema.Description is null
            && GetMemberName(context.JsonTypeInfo, context.JsonPropertyInfo) is { Length: > 0 } memberName
            && GetDescription(memberName) is { Length: > 0 } description
        )
        {
            schema.Description = description;
        }

        return Task.CompletedTask;
    }

    private string? GetDescription(string memberName)
    {
        if (_descriptions.TryGetValue(memberName, out var description))
        {
            return description;
        }

        // Try to find the summary text for the member from the XML documentation file
        var navigator = CreateNavigator();
        var node = navigator.SelectSingleNode($"/doc/members/member[@name='{memberName}']/summary");

        if (node is not null)
        {
            description = node.Value.Trim();
        }

        // Cache the description for this member
        _descriptions[memberName] = description;

        return description;
    }

    private static string? GetMemberName(JsonTypeInfo typeInfo, JsonPropertyInfo? propertyInfo)
    {
        if (typeInfo.Type.Assembly != s_thisAssembly && propertyInfo?.DeclaringType.Assembly != s_thisAssembly)
        {
            // The type or member's type is not from this assembly (e.g. from the framework itself)
            return null;
        }
        else if (propertyInfo is not null)
        {
            // We need to get the summary for the property (or field)
            var typeName = propertyInfo.DeclaringType.FullName?.Replace("+", ".");
            var memberName = propertyInfo.AttributeProvider is MemberInfo member
                ? member.Name
                : $"{char.ToUpperInvariant(propertyInfo.Name[0])}{propertyInfo.Name[1..]}";

            // Is the member a property or a field?
            var memberType = propertyInfo.AttributeProvider is PropertyInfo ? "P" : "F";

            return $"{memberType}:{typeName}{Type.Delimiter}{memberName}";
        }
        else
        {
            // We need to get the summary for the type itself
            return $"T:{typeInfo.Type.FullName?.Replace("+", ".")}";
        }
    }

    private XPathNavigator CreateNavigator()
    {
        if (_navigator is null)
        {
            // Find the .xml documentation file associated with this assembly.
            // It should be in the application's directory next to the .dll file.
            var path = Path.Combine(AppContext.BaseDirectory, $"{s_thisAssembly.GetName().Name}.xml");
            using var reader = XmlReader.Create(path);
            _navigator = new XPathDocument(reader).CreateNavigator();
        }

        return _navigator;
    }
}
