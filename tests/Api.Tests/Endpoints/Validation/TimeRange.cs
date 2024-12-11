using Defra.PhaImportNotifications.Api.Endpoints.ImportNotifications;

namespace Defra.PhaImportNotifications.Api.Tests.Endpoints.Validation;

public record TimeRange(DateTime From, DateTime To) : IDateTimeRangeDefinition;

public class TimeRangeBuilder
{
    private DateTime _from;
    private DateTime _to;

    public TimeRangeBuilder From(DateTime from)
    {
        _from = from;
        return this;
    }

    public TimeRangeBuilder To(DateTime to)
    {
        _to = to;
        return this;
    }

    public TimeRange Build() => new(_from, _to);
}
