using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class CdmsOptions
{
    [Required]
    public required string BaseUrl { get; init; }
}
