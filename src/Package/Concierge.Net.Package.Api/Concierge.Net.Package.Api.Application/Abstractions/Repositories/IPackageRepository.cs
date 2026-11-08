using Concierge.Net.Package.Domain.Packages;

namespace Concierge.Net.Package.Api.Application.Abstractions.Repositories;

public interface IPackageRepository
{
    Task<Domain.Packages.Package?> GetByIdAsync(PackageId id, CancellationToken ct = default);
    void Add(Domain.Packages.Package package);
    void Update(Domain.Packages.Package package);
}
