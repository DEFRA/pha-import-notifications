#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public class Party
{
    [JsonPropertyName("id")]
    [Description("IPAFFS ID of party")]
    public string? Id { get; init; }

    [JsonPropertyName("name")]
    [Description("Name of party")]
    public string? Name { get; init; }

    [JsonPropertyName("companyId")]
    [Description("Company ID")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("contactId")]
    [Description("Contact ID (B2C)")]
    public string? ContactId { get; init; }

    [JsonPropertyName("companyName")]
    [Description("Company name")]
    public string? CompanyName { get; init; }

    [JsonPropertyName("addresses")]
    [Description("Addresses")]
    public List<string>? Addresses { get; init; }

    [JsonPropertyName("county")]
    [Description("County")]
    public string? County { get; init; }

    [JsonPropertyName("postCode")]
    [Description("Post code of party")]
    public string? PostCode { get; init; }

    [JsonPropertyName("country")]
    [Description("Country of party")]
    public string? Country { get; init; }

    [JsonPropertyName("city")]
    [Description("City")]
    public string? City { get; init; }

    [JsonPropertyName("tracesId")]
    [Description("TRACES ID")]
    public int? TracesId { get; init; }

    [JsonPropertyName("type")]
    [Description("Type of party")]
    public PartyTypeEnum? Type { get; init; }

    [JsonPropertyName("approvalNumber")]
    [Description("Approval number")]
    public string? ApprovalNumber { get; init; }

    [JsonPropertyName("phone")]
    [Description("Phone number of party")]
    public string? Phone { get; init; }

    [JsonPropertyName("fax")]
    [Description("Fax number of party")]
    public string? Fax { get; init; }

    [JsonPropertyName("email")]
    [Description("Email number of party")]
    public string? Email { get; init; }
}
