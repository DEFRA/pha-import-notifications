namespace SchemaToCSharp;

internal static class Rename
{
    public static readonly Dictionary<string, string> Types = new(StringComparer.OrdinalIgnoreCase)
    {
        { "CdsClearanceRequest", "CustomsClearanceRequest" },
        { "CdsDecision", "CustomsClearanceDecision" },
    };
}
