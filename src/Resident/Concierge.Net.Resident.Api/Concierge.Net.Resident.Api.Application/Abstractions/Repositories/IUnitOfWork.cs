namespace Concierge.Net.Resident.Api.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken ct = default);
}
