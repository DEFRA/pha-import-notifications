namespace Defra.PhaImportNotifications.Api.Endpoints;

internal sealed class PagedResponse<T>
{
    public IEnumerable<T> Records { get; set; } = [];
    public int TotalRecords { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}