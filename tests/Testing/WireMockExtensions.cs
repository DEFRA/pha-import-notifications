using System.Text.Encodings.Web;
using WireMock.RequestBuilders;

namespace Defra.PhaImportNotifications.Testing;

public static class WireMockExtensions
{
    /// <summary>
    /// A helper for when a JSON.API request param is needed that includes braces. WireMock
    /// does not handle the un-escaping therefore we escape on the key itself so we ensure
    /// a match.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static IRequestBuilder WithJsonApiParam(this IParamsRequestBuilder builder, string key, string value)
    {
        // We use UrlEncoder.Default as the QueryBuilder helper uses that internally
        return builder.WithParam(UrlEncoder.Default.Encode(key), value);
    }
}
