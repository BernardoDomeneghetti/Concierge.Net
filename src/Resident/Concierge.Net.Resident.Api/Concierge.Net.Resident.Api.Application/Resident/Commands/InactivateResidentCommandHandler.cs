using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class InactivateResidentCommandHandler(
    IResidentRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<InactivateResidentCommand, ResidentId>
{
    private readonly IResidentRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResidentId> ExecuteAsync(
        InactivateResidentCommand command,
        CancellationToken correlationToken)
    {
        var residentId = ResidentId.From(command.ResidentId);
        var resident = await _repository.GetByIdAsync(residentId, correlationToken)
            ?? throw new KeyNotFoundException($"Resident '{command.ResidentId}' was not found.");

        resident.Inactivate(
            command.Reason,
            command.InactivatedAtUtc ?? DateTime.UtcNow);

        _repository.Update(resident);

        await _unitOfWork.CommitAsync(correlationToken);

        return resident.Id;
    }
}
