using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

namespace Defra.PhaImportNotifications.Api.Configuration;

public static class SerializerOptions
{
    public static JsonOptions Configure(JsonOptions options)
    {
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());

        return options;
    }
}
