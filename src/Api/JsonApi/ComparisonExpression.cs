namespace Defra.PhaImportNotifications.Api.JsonApi;

public record ComparisonExpression(ComparisonOperator Operator, string Left, string Right)
{
    public override string ToString() => $"{CamelCase(Operator.ToString("G"))}({Left},'{Right.Replace("'", "''")}')";

    private static string CamelCase(string input) => char.ToLower(input[0]) + input[1..];
}
