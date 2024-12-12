using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.BtmsStub;

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
            request = fields.Aggregate(
                request,
                // WireMock does not unescape automatically for us as part of the param scoring/matching
                // therefore the param will come through escaped so we escape the expectation here too
                (builder, field) => builder.WithParam(Uri.EscapeDataString($"{field}"))
            );

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
}
