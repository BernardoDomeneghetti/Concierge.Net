using Concierge.Net.Package.Api.Application.Abstractions.Repositories;
using Concierge.Net.Package.Api.Application.Abstractions.UseCases;
using Concierge.Net.Package.Domain.Packages;

namespace Concierge.Net.Package.Api.Application.Packages.Queries;

public class GetPackageQueryHandler(IPackageRepository repository) : IQueryHandler<GetPackageQuery, GetPackageResponse>
{
    private readonly IPackageRepository _repository = repository;

    public async Task<GetPackageResponse> ExecuteAsync(GetPackageQuery query, CancellationToken correlationToken)
    {
        var packageId = PackageId.From(query.PackageId);
        var package = await _repository.GetByIdAsync(packageId, correlationToken)
            ?? throw new KeyNotFoundException($"Package '{query.PackageId}' was not found.");

        var status = package.IsCancelled
            ? "Cancelled"
            : package.IsCollected
                ? "Collected"
                : "Received";

        return new GetPackageResponse
        {
            Id = package.Id.Value,
            ResidentId = package.ResidentId.Value,
            UnitId = package.UnitId.Value,
            ReceivedAtUtc = package.ReceivedAtUtc,
            CollectedAtUtc = package.CollectedAtUtc,
            CancelledAtUtc = package.CancelledAtUtc,
            Status = status
        };
    }
}
