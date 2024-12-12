using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.BtmsStub;

[ExcludeFromCodeCoverage]
public static class WireMockExtensions
{
    public static void StubSingleImportNotification(
        this WireMockServer wireMock,
        string chedReferenceNumber = ChedReferenceNumbers.ChedA,
        bool shouldFail = false
    )
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
            response = response.WithBody(GetBody("btms-import-notification-single.json"));

        wireMock
            .Given(Request.Create().WithPath(Endpoints.ImportNotifications.Get(chedReferenceNumber)).UsingGet())
            .RespondWith(response);
    }

    public static void StubImportNotificationUpdates(
        this WireMockServer wireMock,
        string? path = null,
        bool shouldFail = false,
        string? filter = null,
        string[]? fields = null,
        Func<JsonNode, JsonNode>? transformBody = null,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null
    )
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
        {
            var body = GetBody("btms-import-notification-updates.json");

            if (transformBody != null)
            {
                var jsonNode = JsonNode.Parse(body);
                if (jsonNode is null)
                    throw new InvalidOperationException("JSON node was null");

                body = transformBody(jsonNode).ToString();
            }

            response = response.WithBody(body);
        }

        var request = Request.Create().WithPath(path ?? Endpoints.ImportNotifications.Get()).UsingGet();

        if (filter is not null)
            request = request.WithParam("filter", MatchBehaviour.AcceptOnMatch, filter);

        if (fields is not null)
            request = fields.Aggregate(request, (builder, field) => builder.WithJsonApiParam(field));

        if (transformRequest is not null)
            request = transformRequest(request);

        wireMock.Given(request).RespondWith(response);
    }

    private static string GetBody(string fileName)
    {
        var type = typeof(WireMockExtensions);
        var assembly = type.Assembly;

        using var stream = assembly.GetManifestResourceStream($"{type.Namespace}.Scenarios.{fileName}");

        if (stream is null)
            throw new InvalidOperationException($"Unable to find embedded resource {fileName}");

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    /// <summary>
    /// A helper for when a JSON.API request param is needed that includes braces. WireMock
    /// does not handle the un-escaping therefore we escape on the param itself so we ensure
    /// a match.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    private static IRequestBuilder WithJsonApiParam(this IParamsRequestBuilder builder, string param)
    {
        return builder.WithParam(Uri.EscapeDataString(param));
    }
}
