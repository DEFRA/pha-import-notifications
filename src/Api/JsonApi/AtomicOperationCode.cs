using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// See "op" in https://jsonapi.org/ext/atomic/#operation-objects.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AtomicOperationCode
{
    Add,
    Update,
    Remove,
}
