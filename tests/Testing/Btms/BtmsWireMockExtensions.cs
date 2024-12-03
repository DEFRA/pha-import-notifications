using Microsoft.AspNetCore.Http;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Defra.PhaImportNotifications.Testing.Btms;

public static class BtmsWireMockExtensions
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
            .Given(Request.Create().WithPath(BtmsEndpoints.ImportNotifications.Get(chedReferenceNumber)).UsingGet())
            .RespondWith(response);
    }

    public static void StubManyImportNotification(this WireMockServer wireMock, bool shouldFail = false)
    {
        var code = shouldFail ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK;
        var response = Response.Create().WithStatusCode(code);

        if (!shouldFail)
            response = response.WithBody(GetBody("btms-import-notification-list.json"));

        wireMock
            .Given(Request.Create().WithPath(BtmsEndpoints.ImportNotifications.Get()).UsingGet())
            .RespondWith(response);
    }

    private static string GetBody(string fileName)
    {
        var type = typeof(BtmsWireMockExtensions);
        var assembly = type.Assembly;

        using var stream = assembly.GetManifestResourceStream($"{type.Namespace}.{fileName}");
        using var reader = new StreamReader(stream!);

        return reader.ReadToEnd();
    }
}
