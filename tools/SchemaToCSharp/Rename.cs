using System.Diagnostics.CodeAnalysis;

namespace SchemaToCSharp;

[ExcludeFromCodeCoverage]
internal static class Rename
{
    public static readonly Dictionary<string, string> Types = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Decision", "ImportDecision" },
    };
}
