using System.ComponentModel.DataAnnotations;

namespace Defra.PhaImportNotifications.Api.Configuration;

public class AclOptions
{
    public Dictionary<string, ClientConfig> Clients { get; init; } = new();

    public class ClientConfig
    {
        [Required]
        public required List<string> Bcps { get; init; }

        [Required]
        public required List<string> ChedTypes { get; init; }
    }
}
