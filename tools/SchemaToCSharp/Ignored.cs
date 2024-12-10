namespace SchemaToCSharp;

internal static class Ignored
{
    public static readonly Dictionary<string, HashSet<string>> Properties = new(StringComparer.OrdinalIgnoreCase)
    {
        {
            "ImportNotification",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "_Etag",
                "_Ts",
                "_PointOfEntry",
                "_PointOfEntryControlPoint",
                "_MatchReference",
                "AuditEntries",
                "Relationships",
                "IsBulkUploadInProgress",
            }
        },
        {
            "PartOne",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "TypeOfImp",
                "CustomsReferenceNumber",
                "ContainsWoodPackaging",
                "ConsignorTwo",
                "PlaceOfOriginHarvest",
                "AdditionalPermanentAddresses",
                "ImportingFromCharity",
                "IsPlaceOfDestinationThePermanentAddress",
                "ResponsibleForTransport",
                "SealsContainers",
                "ComplexCommoditySelected",
            }
        },
        {
            "PartTwo",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ResealedContainers" }
        },
        {
            "VeterinaryInformation",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IdentificationDetails" }
        },
        {
            "ComplementParameterSet",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "CatchCertificates" }
        },
        {
            "CommodityRiskResult",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IsWoody", "IndoorOutdoor" }
        },
        {
            "ControlAuthority",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IuuFishingReference" }
        },
        {
            "ImportNotificationTypeEnum",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IMP" }
        },
        {
            "ImportNotificationStatusEnum",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Draft" }
        },
    };
}
