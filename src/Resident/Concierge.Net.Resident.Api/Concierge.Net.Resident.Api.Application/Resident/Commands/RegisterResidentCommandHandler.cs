using Concierge.Net.Resident.Api.Application.Abstractions.Repositories;
using Concierge.Net.Resident.Api.Application.Abstractions.UseCases;
using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class RegisterResidentCommandHandler(
    IResidentRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterResidentCommand, ResidentId>
{
    private readonly IResidentRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResidentId> ExecuteAsync(
        RegisterResidentCommand command,
        CancellationToken correlationToken)
    {
        var resident = Domain.Residents.Resident.Register(
            command.FullName,
            Cpf.From(command.Cpf),
            Email.From(command.Email),
            PhoneNumber.From(command.MobilePhone),
            command.PhotoUrl,
            UnitId.From(command.UnitId),
            command.ResidentType,
            command.NotificationChannel);

        _repository.Add(resident);

        await _unitOfWork.CommitAsync(correlationToken);

        return resident.Id;
    }
}
