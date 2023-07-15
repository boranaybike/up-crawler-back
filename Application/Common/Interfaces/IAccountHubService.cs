public interface IAccountHubService
{
    Task RemovedAsync(Guid id, CancellationToken cancellationToken);
}