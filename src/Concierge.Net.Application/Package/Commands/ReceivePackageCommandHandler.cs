using Concierge.Net.Application.Abstractions.Repositories;
using Concierge.Net.Application.Abstractions.UseCases;
using Concierge.Net.Domain.Packages;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.Packages.Commands;

public class ReceivePackageCommandHandler(
    IPackageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<ReceivePackageCommand, PackageId>
{
    private readonly IPackageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PackageId> ExecuteAsync(
        ReceivePackageCommand command,  
        CancellationToken correlationToken)
    {
        var package = Package.Receive(
            ResidentId.From(command.ResidentId),
            Email.From(command.Email),
            UnitId.From(command.UnitId)
        ); 

        _repository.Add(package);

        await _unitOfWork.CommitAsync(correlationToken);

        return package.Id;
    }
}
