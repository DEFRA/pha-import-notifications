namespace Defra.PhaImportNotifications.Api.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> NotNull<T>(this IEnumerable<T?> source) => source.Where(x => x is not null)!;

    public static IEnumerable<T> ThrowIfAnyNull<T>(this IEnumerable<T?> source, string message)
    {
        var list = source.ToList();

        if (list.Any(x => x is null))
            throw new InvalidOperationException(message);

        return list.NotNull();
    }
}
