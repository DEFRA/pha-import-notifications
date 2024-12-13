using System.ComponentModel.DataAnnotations;
using Defra.PhaImportNotifications.Api.Helpers;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class BtmsOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required string Username { get; init; }

    public string BasicAuthCredential => BasicAuthHelper.CreateBasicAuth(Username, Password);

    public int PageSize { get; init; } = 1000;
}
