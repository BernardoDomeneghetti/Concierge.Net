using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Domain.Residents;
using MongoDB.Driver;

namespace Concierge.Net.Resident.Api.Infraestructure.Persistence;

public class ResidentRepository(IMongoDatabase database, UnitOfWork unitOfWork) : IResidentRepository
{
    private readonly IMongoCollection<Domain.Residents.Resident> _collection = database.GetCollection<Domain.Residents.Resident>("residents");
    private readonly UnitOfWork _unitOfWork = unitOfWork;

    public async Task<Domain.Residents.Resident?> GetByIdAsync(ResidentId id, CancellationToken ct = default)
    {
        return await _collection
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync(ct);
    }

    public void Add(Domain.Residents.Resident resident)
    {
        _unitOfWork.RegisterOperation(
            aggregate: resident,
            operation: (session, ct) => _collection.InsertOneAsync(session, resident, cancellationToken: ct)
        );
    }

    public void Update(Domain.Residents.Resident resident)
    {
        _unitOfWork.RegisterOperation(
            aggregate: resident,
            operation: (session, ct) => _collection.ReplaceOneAsync(
                session,
                r => r.Id == resident.Id,
                resident,
                cancellationToken: ct)
        );
    }
}
