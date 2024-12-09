namespace Defra.PhaImportNotifications.Api.JsonApi;

public record ComparisonExpression(ComparisonOperator Operator, string Field, string Value) : IExpression
{
    public override string ToString() => $"{CamelCase(Operator.ToString("G"))}({Field},'{Escape(Value)}')";

    private static string Escape(string value) => value.Replace("'", "''");

    private static string CamelCase(string input) => char.ToLower(input[0]) + input[1..];
}
