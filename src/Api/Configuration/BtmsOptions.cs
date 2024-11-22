using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class BtmsOptions
{
    [Required]
    public required string BaseUrl { get; init; }
}
