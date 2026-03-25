using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.Repositories;
using Concierge.Net.Application.Abstractions.UseCases;
using Concierge.Net.Domain.Packages;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.Package.Commands;

public class ReceivePackageCommandHandler: ICommandHandler<ReceivePackageCommand, PackageId>
{
    private readonly IPackageRepository _repository;
    private readonly IDomainEventDispatcher _dispatcher;

    public ReceivePackageCommandHandler(IPackageRepository repository, IDomainEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public async Task<PackageId> ExecuteAsync(
        ReceivePackageCommand command,  
        CancellationToken correlationToken)
    {
            
        var package = Domain.Packages.Package.Receive(//TODO: Remove fully qualified name once Package is being imported 
            ResidentId.From(command.ResidentId),
            Email.From(command.Email),
            UnitId.From(command.UnitId)
        ); 

        await _repository.SaveAsync(package, correlationToken);

        await _dispatcher.DispatchAsync(package.DomainEvents, correlationToken);

        package.ClearDomainEvents();

        return package.Id;
    }
}