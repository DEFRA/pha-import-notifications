namespace Defra.PhaImportNotifications.Api.JsonApi;

public record FieldExpression(string ResourceType, string[] Fields)
{
    public override string ToString() =>
        Fields.Length == 0 ? string.Empty : $"fields[{ResourceType}]={string.Join(",", Fields)}";

    public (string Key, string Value)? ToParts() =>
        Fields.Length == 0 ? null : ($"fields[{ResourceType}]", string.Join(",", Fields));
}
