using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

public abstract class JsonObjectConverter<TObject> : JsonConverter<TObject>
{
    protected static TValue? ReadSubTree<TValue>(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (typeof(TValue) != typeof(object) && options.GetConverter(typeof(TValue)) is JsonConverter<TValue> converter)
        {
            return converter.Read(ref reader, typeof(TValue), options);
        }

        return JsonSerializer.Deserialize<TValue>(ref reader, options);
    }
}
