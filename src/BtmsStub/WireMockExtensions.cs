using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.BtmsStub;

[ExcludeFromCodeCoverage]
public static class WireMockExtensions
{
    public static void StubSingleImportNotification(
        this WireMockServer wireMock,
        bool shouldFail = false,
        string chedReferenceNumber = ChedReferenceNumbers.ChedA,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null,
        Func<JsonNode, JsonNode>? transformResponse = null
    )
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
        {
            var responseBody = GetBody($"btms-import-notification-single-{chedReferenceNumber}.json");
            if (transformResponse is not null)
                responseBody = transformResponse(JsonNode.Parse(responseBody)!).ToJsonString();

            response = response.WithBody(responseBody);
        }

        var request = Request.Create().WithPath(Endpoints.ImportNotifications.Get(chedReferenceNumber)).UsingGet();

        if (transformRequest is not null)
            request = transformRequest(request);

        wireMock.Given(request).RespondWith(response);
    }

    public static void StubImportNotificationUpdates(
        this WireMockServer wireMock,
        bool shouldFail = false,
        Func<JsonNode, JsonNode>? transformBody = null,
        string? path = null,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null,
        int? statusCode = null
    )
    {
        var code = statusCode ?? (shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK);
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

        if (transformRequest is not null)
            request = transformRequest(request);

        wireMock.Given(request).RespondWith(response);
    }

    public static void StubSingleMovement(
        this WireMockServer wireMock,
        bool shouldFail = false,
        string mrn = MovementReferenceNumbers.Movement1,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null
    )
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
            response = response.WithBody(GetBody($"btms-movement-single-{mrn}.json"));

        var request = Request.Create().WithPath(Endpoints.Movements.Get(mrn)).UsingGet();

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
