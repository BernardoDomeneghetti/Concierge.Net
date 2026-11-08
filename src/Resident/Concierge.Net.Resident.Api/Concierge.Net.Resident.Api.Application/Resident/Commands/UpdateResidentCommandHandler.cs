using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class UpdateResidentCommandHandler(
    IResidentRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateResidentCommand, ResidentId>
{
    private readonly IResidentRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResidentId> ExecuteAsync(
        UpdateResidentCommand command,
        CancellationToken correlationToken)
    {
        var residentId = ResidentId.From(command.ResidentId);
        var resident = await _repository.GetByIdAsync(residentId, correlationToken)
            ?? throw new KeyNotFoundException($"Resident '{command.ResidentId}' was not found.");

        resident.UpdateProfile(
            command.FullName,
            Email.From(command.Email),
            PhoneNumber.From(command.MobilePhone),
            command.PhotoUrl,
            UnitId.From(command.UnitId),
            command.ResidentType,
            command.NotificationChannel,
            command.UpdatedAtUtc ?? DateTime.UtcNow);

        _repository.Update(resident);

        await _unitOfWork.CommitAsync(correlationToken);

        return resident.Id;
    }
}
