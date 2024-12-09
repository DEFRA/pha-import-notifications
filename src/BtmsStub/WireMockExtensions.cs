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

    public static void StubManyImportNotification(
        this WireMockServer wireMock,
        bool shouldFail = false,
        string? filter = null
    )
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
            response = response.WithBody(GetBody("btms-import-notification-list.json"));

        var request = Request.Create().WithPath(Endpoints.ImportNotifications.Get()).UsingGet();

        if (filter is not null)
            request = request.WithParam("filter", MatchBehaviour.AcceptOnMatch, filter);

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
