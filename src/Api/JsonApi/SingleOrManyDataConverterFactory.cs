using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Defra.PhaImportNotifications.Api.JsonApi;

/// <summary>
///     Converts <see cref="SingleOrManyData{T}" /> to/from JSON.
/// </summary>
public sealed class SingleOrManyDataConverterFactory : JsonConverterFactory
{
    /// <inheritdoc />
    public override bool CanConvert(Type typeToConvert)
    {
        ArgumentNullException.ThrowIfNull(typeToConvert);

        return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(SingleOrManyData<>);
    }

    /// <inheritdoc />
    public override System.Text.Json.Serialization.JsonConverter CreateConverter(
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        ArgumentNullException.ThrowIfNull(typeToConvert);

        var objectType = typeToConvert.GetGenericArguments()[0];
        var converterType = typeof(SingleOrManyDataConverter<>).MakeGenericType(objectType);

        return (System.Text.Json.Serialization.JsonConverter)Activator.CreateInstance(converterType)!;
    }

    private sealed class SingleOrManyDataConverter<T> : JsonObjectConverter<SingleOrManyData<T>>
        where T : ResourceIdentifierObject, new()
    {
        public override SingleOrManyData<T> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            List<T?> objects = [];
            var isManyData = false;
            var hasCompletedToMany = false;

            do
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.EndArray:
                    {
                        hasCompletedToMany = true;
                        break;
                    }
                    case JsonTokenType.StartObject:
                    {
                        var resourceObject = ReadSubTree<T>(ref reader, options);
                        objects.Add(resourceObject);
                        break;
                    }
                    case JsonTokenType.StartArray:
                    {
                        isManyData = true;
                        break;
                    }
                }
            } while (isManyData && !hasCompletedToMany && reader.Read());

            object? data = isManyData ? objects : objects.FirstOrDefault();
            return new SingleOrManyData<T>(data);
        }

        [ExcludeFromCodeCoverage]
        public override void Write(Utf8JsonWriter writer, SingleOrManyData<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
