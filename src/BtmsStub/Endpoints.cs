namespace Defra.PhaImportNotifications.BtmsStub;

public static class Endpoints
{
    public static class ImportNotifications
    {
        public static string Get(string? chedReferenceNumber = null) =>
            $"/api/import-notifications{(chedReferenceNumber is null ? string.Empty : $"/{chedReferenceNumber}")}";
    }
}
