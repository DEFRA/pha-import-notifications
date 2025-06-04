using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class TradeImportsDataApiOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required string Username { get; init; }

    public int PageSize { get; init; } = 1000;
}
