using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.Repositories;
using Concierge.Net.Application.Abstractions.UseCases;
using Concierge.Net.Domain.Packages;
using Concierge.Net.Domain.Residents;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.UseCases.Package;

public class ReceivePackageUseCase: IReceveivePackageUseCase
{
    private readonly IPackageRepository _repository;
    private readonly IDomainEventDispatcher _dispatcher;

    public ReceivePackageUseCase(IPackageRepository repository, IDomainEventDispatcher dispatcher)
    {
        _repository = repository;
        _dispatcher = dispatcher;
    }

    public async Task ExecuteAsync(
        ResidentId residentId,
        UnitId unitId,
        Email email,
        CancellationToken correlationToken)
    {
        var package = Domain.Packages.Package.Receive(residentId, email, unitId); //TODO: Remove fully qualified name once Package is being imported 

        await _repository.SaveAsync(package, correlationToken);

        await _dispatcher.DispatchAsync(package.DomainEvents, correlationToken);

        package.ClearDomainEvents();
    }
}