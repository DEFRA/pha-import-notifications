using WireMock.RequestBuilders;

namespace Defra.PhaImportNotifications.Testing;

public static class WireMockExtensions
{
    /// <summary>
    /// A helper for when a JSON.API request param is needed that includes braces. WireMock
    /// does not handle the un-escaping therefore we escape on the param itself so we ensure
    /// a match.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static IRequestBuilder WithJsonApiParam(this IParamsRequestBuilder builder, string param)
    {
        return builder.WithParam(Uri.EscapeDataString(param));
    }
}
