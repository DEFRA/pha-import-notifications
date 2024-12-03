#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class EconomicOperator
{
    [JsonPropertyName("id")]
    [Description("The unique identifier of this organisation")]
    public string? Id { get; init; }

    [JsonPropertyName("type")]
    [Description("Type of organisation")]
    public EconomicOperatorTypeEnum? Type { get; init; }

    [JsonPropertyName("status")]
    [Description("Status of organisation")]
    public EconomicOperatorStatusEnum? Status { get; init; }

    [JsonPropertyName("companyName")]
    [Description("Name of organisation")]
    public string? CompanyName { get; init; }

    [JsonPropertyName("individualName")]
    [Description("Individual name")]
    public string? IndividualName { get; init; }

    [JsonPropertyName("address")]
    [Description("Address of economic operator")]
    public Address? Address { get; init; }

    [JsonPropertyName("approvalNumber")]
    [Description("Approval Number which identifies an Economic Operator unambiguously per type of organisation per country.")]
    public string? ApprovalNumber { get; init; }

    [JsonPropertyName("otherIdentifier")]
    [Description("Optional Business General Number, often named Aggregation Code, which identifies an Economic Operator.")]
    public string? OtherIdentifier { get; init; }

    [JsonPropertyName("tracesId")]
    [Description("Traces Id of the economic operator generated by IPAFFS")]
    public int? TracesId { get; init; }
}
