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

    public int StubPort { get; init; } = 8090;

    [Required]
    public required string Username { get; init; }

    public string BasicAuthCredential => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));
}
