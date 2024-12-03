using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class BtmsOptions
{
    [Required]
    public required string BaseUrl { get; init; }

    [Required]
    public required string Password { get; init; }

    public bool StubEnabled { get; init; } = false;

    [Required]
    public required string Username { get; init; }

    public string BasicAuthCredential => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));
}
