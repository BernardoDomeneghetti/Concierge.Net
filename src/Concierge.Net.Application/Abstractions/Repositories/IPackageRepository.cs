using System;

namespace Concierge.Net.Application.Abstractions.Repositories;

public interface IPackageRepository
{
    Task SaveAsync(Domain.Packages.Package package, CancellationToken ct);
}
