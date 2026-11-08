using Concierge.Net.Package.Api.Application.Abstractions.Repositories;
using Concierge.Net.Package.Api.Application.Abstractions.UseCases;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Domain.Residents;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Api.Application.Packages.Commands;

public class CollectPackageCommandHandler(
    IPackageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CollectPackageCommand, PackageId>
{
    private readonly IPackageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PackageId> ExecuteAsync(
        CollectPackageCommand command,
        CancellationToken correlationToken)
    {
        var packageId = PackageId.From(command.PackageId);
        var package = await _repository.GetByIdAsync(packageId, correlationToken)
            ?? throw new KeyNotFoundException($"Package '{command.PackageId}' was not found.");

        package.MarkAsCollected(
            ResidentId.From(command.CollectedByResidentId),
            command.SignatureHash,
            command.CollectedAtUtc ?? DateTime.UtcNow,
            Email.From(command.Email));

        _repository.Update(package);

        await _unitOfWork.CommitAsync(correlationToken);

        return package.Id;
    }
}

