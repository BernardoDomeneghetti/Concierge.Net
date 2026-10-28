using Concierge.Net.Application.Abstractions.Repositories;
using Concierge.Net.Domain.Packages;
using MongoDB.Driver;

namespace Concierge.Net.Infraestructure.Persistence;

public class PackageRepository(IMongoDatabase database, UnitOfWork unitOfWork) : IPackageRepository
{
    private readonly IMongoCollection<Package> _collection = database.GetCollection<Package>("packages");
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public void Add(Package package)
    {
        _unitOfWork.RegisterOperation(
            aggregate: package,
            operation: (session, ct) => _collection.InsertOneAsync(session, package, cancellationToken: ct)
        );
    }
}
