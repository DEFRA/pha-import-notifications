namespace SchemaToCSharp;

internal static class Meta
{
    public static readonly Dictionary<string, Dictionary<string, string>> Descriptions = new(
        StringComparer.OrdinalIgnoreCase
    )
    {
        {
            "ImportNotification",
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Created", "Date when the notification was created" },
                { "Updated", "Date when the notification was last updated" },
                { "CreatedSource", "Date when the notification was created in IPAFFS" },
                { "UpdatedSource", "Date when the notification was last updated in IPAFFS" },
            }
        },
        {
            "Movement",
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Created", "Date when the movement was created" },
                { "Updated", "Date when the movement was last updated" },
                { "CreatedSource", "Date when the movement was created in ALVS" },
                { "UpdatedSource", "Date when the movement was last updated in ALVS" },
            }
        },
    };
}
