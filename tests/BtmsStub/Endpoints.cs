namespace Defra.PhaImportNotifications.BtmsStub;

public static class Endpoints
{
    public static class ImportNotifications
    {
        public static string Get(string? chedReferenceNumber = null) =>
            $"/api/import-notifications{(chedReferenceNumber is null ? string.Empty : $"/{chedReferenceNumber}")}";
    }

    public static class Movements
    {
        public static string Get(string? mrn = null) => $"/api/movements{(mrn is null ? string.Empty : $"/{mrn}")}";
    }

    public static class Gmrs
    {
        public static string Get(string? gmrId = null) => $"/api/gmrs{(gmrId is null ? string.Empty : $"/{gmrId}")}";
    }
}
