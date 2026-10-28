using Concierge.Net.Domain.Packages;

namespace Concierge.Net.Application.Abstractions.Repositories;

public interface IPackageRepository
{
    void Add(Package package);
}
