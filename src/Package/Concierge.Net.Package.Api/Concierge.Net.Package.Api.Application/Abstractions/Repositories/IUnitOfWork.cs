namespace Concierge.Net.Package.Api.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct = default);
}
