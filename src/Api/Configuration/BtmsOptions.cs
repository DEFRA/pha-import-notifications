using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class BtmsOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required string Username { get; init; }

    public string BasicAuthCredential => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));

    public int PageSize { get; init; } = 1000;
}
