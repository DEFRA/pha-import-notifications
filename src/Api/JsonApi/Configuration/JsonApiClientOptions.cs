namespace Defra.PhaImportNotifications.Api.JsonApi.Configuration;

public class JsonApiClientOptions
{
    public required string BaseUrl { get; set; }
    public string? BasicAuthCredential { get; set; }
}
