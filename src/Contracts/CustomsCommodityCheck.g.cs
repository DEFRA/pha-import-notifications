#nullable enable
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Defra.PhaImportNotifications.Contracts;
public partial record CustomsCommodityCheck
{
    [JsonPropertyName("checkCode")]
    public string? CheckCode { get; init; }

    [JsonPropertyName("departmentCode")]
    public string? DepartmentCode { get; init; }
}
