namespace Defra.PhaImportNotifications.Api.JsonApi;

public record NotExpression(ComparisonExpression Expression) : IExpression
{
    public override string ToString() => $"not({Expression})";
}
