namespace Defra.PhaImportNotifications.Api.JsonApi;

public interface IJsonApiClient
{
    Task<Document?> Get(RequestUri requestUri, CancellationToken cancellationToken);
    Task<Document?> Get(string requestUri, CancellationToken cancellationToken);
}