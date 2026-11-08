using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Abstractions.Repositories;

public interface IResidentRepository
{
    Task<Domain.Residents.Resident?> GetByIdAsync(ResidentId id, CancellationToken ct = default);
    void Add(Domain.Residents.Resident resident);
    void Update(Domain.Residents.Resident resident);
}
