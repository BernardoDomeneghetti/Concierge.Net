namespace Concierge.Net.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct = default);
}
