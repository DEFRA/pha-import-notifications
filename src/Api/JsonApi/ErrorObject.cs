using System.Net;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See https://jsonapi.org/format/#error-objects.
/// </summary>
public sealed class ErrorObject(HttpStatusCode statusCode)
{
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; } = Guid.NewGuid().ToString();

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; set; } = statusCode;

    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Status
    {
        get => StatusCode.ToString("d");
        set => StatusCode = (HttpStatusCode)int.Parse(value);
    }

    [JsonPropertyName("title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    [JsonPropertyName("source")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ErrorSource? Source { get; set; }
}
