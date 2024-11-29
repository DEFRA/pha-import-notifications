using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
/// Represents the value of the "data" element, which is either null, a single object or an array of objects. Add
/// <see cref="SingleOrManyDataConverterFactory" /> to <see cref="JsonSerializerOptions.Converters" /> to properly roundtrip.
/// </summary>
/// <typeparam name="T">
/// The type of elements being wrapped, typically <see cref="ResourceIdentifierObject" /> or <see cref="ResourceObject" />.
/// </typeparam>
public readonly struct SingleOrManyData<T>
    // The "new()" constraint exists for parity with SingleOrManyDataConverterFactory, which creates empty instances
    // to ensure ManyValue never contains null items.
    where T : ResourceIdentifierObject, new()
{
    [JsonIgnore]
    public T? SingleValue { get; }

    [JsonIgnore]
    public IList<T>? ManyValue { get; }

    public SingleOrManyData(object? value)
    {
        if (value is IEnumerable<T> manyData)
        {
            ManyValue = manyData.ToList();
            SingleValue = null;
        }
        else
        {
            ManyValue = null;
            SingleValue = (T?)value;
        }
    }
}
