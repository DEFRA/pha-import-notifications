namespace Defra.PhaImportNotifications.Api.JsonApi;

public record AnyExpression(string Field, string[] Values) : IExpression
{
    public override string ToString() =>
        Values.Length == 0 ? string.Empty : $"any({Field},{string.Join(",", Values.Select(x => $"'{Escape(x)}'"))})";

    private static string Escape(string value) => value.Replace("'", "''");
}
