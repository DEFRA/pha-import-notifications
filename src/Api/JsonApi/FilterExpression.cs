namespace Defra.PhaImportNotifications.Api.JsonApi;

public record FilterExpression(
    LogicalOperator Operator,
    IEnumerable<IExpression> Expressions,
    IEnumerable<FilterExpression>? NestedExpressions = null
)
{
    public override string ToString()
    {
        var expressions = Expressions
            .Select(x => x.ToString())
            .Concat((NestedExpressions ?? []).Select(x => x.ToString()))
            .Where(x => !string.IsNullOrEmpty(x))
            .ToArray();

        if (expressions.Length == 0)
            return string.Empty;

        return expressions.Length > 1
            ? $"{Operator.ToString("G").ToLower()}({string.Join(",", expressions)})"
            : expressions[0];
    }
}
