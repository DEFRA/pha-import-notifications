using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Nodes;
using Defra.PhaImportNotifications.Api.TradeDataApi;
using Microsoft.AspNetCore.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.BtmsStub;

[ExcludeFromCodeCoverage]
public static class WireMockExtensions
{
    private static Type Anchor => typeof(WireMockExtensions);

    public static void StubImportNotificationAndSubPaths(
        this WireMockServer wireMock,
        string chedReferenceNumber,
        Func<JsonNode, JsonNode>? transformImportNotificationResponse = null,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null
    )
    {
        wireMock.StubImportNotification(chedReferenceNumber, transformRequest, transformImportNotificationResponse);
        wireMock.StubImportNotificationCustomsDeclarations(chedReferenceNumber);
        wireMock.StubImportNotificationGoodsMovements(chedReferenceNumber);
    }

    private static void StubImportNotification(
        this WireMockServer wireMock,
        string chedReferenceNumber,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null,
        Func<JsonNode, JsonNode>? transformResponse = null
    )
    {
        var responseBody = GetBody($"_import-pre-notifications_{chedReferenceNumber}.json");

        if (transformResponse is not null)
            responseBody = transformResponse(JsonNode.Parse(responseBody)!).ToJsonString();

        var response = Response.Create().WithBody(responseBody).WithStatusCode(StatusCodes.Status200OK);

        var request = Request
            .Create()
            .WithPath(TradeDataHttpClient.Endpoints.ImportNotification(chedReferenceNumber))
            .UsingGet();

        if (transformRequest is not null)
            request = transformRequest(request);

        wireMock.Given(request).RespondWith(response);
    }

    public static void StubImportNotificationCustomsDeclarations(
        this WireMockServer wireMock,
        string chedReferenceNumber
    )
    {
        var responseBody = GetBody($"_import-pre-notifications_{chedReferenceNumber}_customs-declarations.json");

        wireMock
            .Given(
                Request
                    .Create()
                    .WithPath(TradeDataHttpClient.Endpoints.CustomsDeclarations(chedReferenceNumber))
                    .UsingGet()
            )
            .RespondWith(Response.Create().WithBody(responseBody).WithStatusCode(StatusCodes.Status200OK));
    }

    public static void StubImportNotificationGoodsMovements(this WireMockServer wireMock, string chedReferenceNumber)
    {
        var responseBody = GetBody($"_import-pre-notifications_{chedReferenceNumber}_gmrs.json");

        wireMock
            .Given(
                Request.Create().WithPath(TradeDataHttpClient.Endpoints.GoodsMovements(chedReferenceNumber)).UsingGet()
            )
            .RespondWith(Response.Create().WithBody(responseBody).WithStatusCode(StatusCodes.Status200OK));
    }

    public static void StubImportNotificationUpdates(
        this WireMockServer wireMock,
        bool shouldFail = false,
        Func<JsonNode, JsonNode>? transformBody = null,
        Func<IRequestBuilder, IRequestBuilder>? transformRequest = null,
        int? statusCode = null
    )
    {
        var code = statusCode ?? (shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK);
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
        {
            var body = GetUpdatesBody("btms-import-notification-updates.json");

            if (transformBody != null)
            {
                var jsonNode = JsonNode.Parse(body);
                if (jsonNode is null)
                    throw new InvalidOperationException("JSON node was null");

                body = transformBody(jsonNode).ToString();
            }

            response = response.WithBody(body);
        }

        var request = Request.Create().WithPath("/api/import-notifications").UsingGet();

        if (transformRequest is not null)
            request = transformRequest(request);

        wireMock.Given(request).RespondWith(response);
    }

    public static IEnumerable<string> GetAllStubChedReferenceNumbers() =>
        Anchor
            .Assembly.GetManifestResourceNames()
            .Where(x => x.StartsWith($"{Anchor.Namespace}.TradeDataApiScenarios._import-pre-notifications_"))
            .Select(x =>
                x.Replace($"{Anchor.Namespace}.TradeDataApiScenarios._import-pre-notifications_", "")
                    .Replace(".json", "")
            )
            .Where(x => char.IsDigit(x.Last()));

    private static string GetUpdatesBody(string fileName) =>
        GetManifestResource($"{Anchor.Namespace}.Scenarios.{fileName}");

    private static string GetBody(string fileName) =>
        GetManifestResource($"{Anchor.Namespace}.TradeDataApiScenarios.{fileName}");

    private static string GetManifestResource(string name)
    {
        using var stream = Anchor.Assembly.GetManifestResourceStream(name);

        if (stream is null)
            throw new InvalidOperationException($"Unable to find embedded resource {name}");

        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }
}
