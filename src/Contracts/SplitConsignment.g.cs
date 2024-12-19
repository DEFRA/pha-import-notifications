#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial class SplitConsignment
{
    [JsonPropertyName("validReferenceNumber")]
    [Description("Reference number of the valid split consignment")]
    public string? ValidReferenceNumber { get; init; }

    [JsonPropertyName("rejectedReferenceNumber")]
    [Description("Reference number of the rejected split consignment")]
    public string? RejectedReferenceNumber { get; init; }
}
