using Concierge.Net.Package.Api.Application.Abstractions.Repositories;
using Concierge.Net.Package.Api.Application.Abstractions.UseCases;
using Concierge.Net.Package.Domain.Packages;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Api.Application.Packages.Commands;

public class CancelPackageCommandHandler(
    IPackageRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CancelPackageCommand, PackageId>
{
    private readonly IPackageRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<PackageId> ExecuteAsync(
        CancelPackageCommand command,
        CancellationToken correlationToken)
    {
        var packageId = PackageId.From(command.PackageId);
        var package = await _repository.GetByIdAsync(packageId, correlationToken)
            ?? throw new KeyNotFoundException($"Package '{command.PackageId}' was not found.");

        package.Cancel(
            command.Reason,
            command.CancelledAtUtc ?? DateTime.UtcNow,
            Email.From(command.Email));

        _repository.Update(package);

        await _unitOfWork.CommitAsync(correlationToken);

        return package.Id;
    }
}

