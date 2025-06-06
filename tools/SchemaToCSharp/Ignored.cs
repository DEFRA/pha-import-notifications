using System.Diagnostics.CodeAnalysis;

namespace SchemaToCSharp;

[ExcludeFromCodeCoverage]
internal static class Ignored
{
    public static readonly Dictionary<string, HashSet<string>> Properties = new(StringComparer.OrdinalIgnoreCase)
    {
        {
            "ImportPreNotification",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IsBulkUploadInProgress" }
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
            "ComplementParameterSets",
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
            "ImportPreNotification.importNotificationType",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "IMP" }
        },
        {
            "ImportPreNotification.status",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Draft", "SUBMITTED,IN_PROGRESS,MODIFY" }
        },
        {
            "ClearanceRequest",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "DeclarationUcr",
                "DeclarationPartNumber",
                "DeclarationType",
                "SubmitterTurn",
                "DeclarantId",
                "DeclarantName",
                "MasterUcr",
            }
        },
        {
            "ImportDocument",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "DocumentStatus",
                "DocumentControl",
                "DocumentQuantity",
            }
        },
        {
            "Gmr",
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                // Not required/requested
                "checkedInCrossing",
                "inspectionRequired",
                "haulierEORI",
                "reportToLocations",
                "direction",
                "haulierType",
            }
        },
    };
}
