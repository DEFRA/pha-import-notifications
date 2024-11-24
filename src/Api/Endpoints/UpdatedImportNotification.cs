using System.ComponentModel;

namespace Defra.PhaImportNotifications.Api.Endpoints;

public class UpdatedImportNotification
{
    [Description("Last updated date. Format is ISO 8601-1:2019")]
    public DateTime LastUpdated { get; init; }

    [Description("Relative path to import notification")]
    public required Uri Uri { get; init; }
}
