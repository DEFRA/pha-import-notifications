namespace Defra.PhaImportNotifications.Testing.Btms;

public static class BtmsEndpoints
{
    public static class ImportNotifications
    {
        public static string Get(string? chedReferenceNumber = null) =>
            $"/api/import-notifications{(chedReferenceNumber is null ? string.Empty : $"/{chedReferenceNumber}")}";
    }
}
