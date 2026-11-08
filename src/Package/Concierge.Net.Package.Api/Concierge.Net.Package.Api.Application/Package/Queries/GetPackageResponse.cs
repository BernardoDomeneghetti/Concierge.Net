namespace Concierge.Net.Package.Api.Application.Packages.Queries;

public class GetPackageResponse
{
    public Guid Id { get; init; }
    public Guid ResidentId { get; init; }
    public Guid UnitId { get; init; }
    public DateTime ReceivedAtUtc { get; init; }
    public DateTime? CollectedAtUtc { get; init; }
    public DateTime? CancelledAtUtc { get; init; }
    public string Status { get; init; } = string.Empty;
}
