using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Residents.Queries;

public class GetResidentQueryHandler(IResidentRepository repository) : IQueryHandler<GetResidentQuery, GetResidentResponse>
{
    private readonly IResidentRepository _repository = repository;

    public async Task<GetResidentResponse> ExecuteAsync(GetResidentQuery query, CancellationToken correlationToken)
    {
        var residentId = ResidentId.From(query.ResidentId);
        var resident = await _repository.GetByIdAsync(residentId, correlationToken)
            ?? throw new KeyNotFoundException($"Resident '{query.ResidentId}' was not found.");

        return new GetResidentResponse
        {
            Id = resident.Id.Value,
            FullName = resident.FullName,
            Cpf = resident.Cpf.Value,
            Email = resident.Email.Value,
            MobilePhone = resident.MobilePhone.Value,
            PhotoUrl = resident.PhotoUrl,
            BiometricSignature = resident.BiometricSignature,
            UnitId = resident.UnitId.Value,
            ResidentType = resident.ResidentType,
            NotificationChannel = resident.NotificationChannel,
            CreatedAtUtc = resident.CreatedAtUtc,
            UpdatedAtUtc = resident.UpdatedAtUtc,
            InactivatedAtUtc = resident.InactivatedAtUtc,
            InactivationReason = resident.InactivationReason,
            Status = resident.IsActive ? "Active" : "Inactive"
        };
    }
}
