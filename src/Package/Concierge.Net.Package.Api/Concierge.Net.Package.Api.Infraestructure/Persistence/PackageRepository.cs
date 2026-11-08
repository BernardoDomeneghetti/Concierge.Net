using Concierge.Net.Package.Api.Application.Abstractions.Repositories;
using Concierge.Net.Package.Domain.Packages;
using MongoDB.Driver;

namespace Concierge.Net.Package.Api.Infraestructure.Persistence;

public class PackageRepository(IMongoDatabase database, UnitOfWork unitOfWork) : IPackageRepository
{
    private readonly IMongoCollection<Domain.Packages.Package> _collection = database.GetCollection<Domain.Packages.Package>("packages");
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public async Task<Domain.Packages.Package?> GetByIdAsync(PackageId id, CancellationToken ct = default)
    {
        return await _collection
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public void Add(Domain.Packages.Package package)
    {
        _unitOfWork.RegisterOperation(
            aggregate: package,
            operation: (session, ct) => _collection.InsertOneAsync(session, package, cancellationToken: ct)
        );
    }

    public void Update(Domain.Packages.Package package)
    {
        _unitOfWork.RegisterOperation(
            aggregate: package,
            operation: (session, ct) => _collection.ReplaceOneAsync(
                session,
                p => p.Id == package.Id,
                package,
                cancellationToken: ct)
        );
    }
}
