namespace Concierge.Net.Package.Api.Contracts;

public sealed record ReceivePackageRequest(
    Guid ResidentId,
    Guid UnitId,
    string Email);

public sealed record CollectPackageRequest(
    Guid CollectedByResidentId,
    string SignatureHash,
    string Email,
    DateTime? CollectedAtUtc = null);

public sealed record CancelPackageRequest(
    string Reason,
    string Email,
    DateTime? CancelledAtUtc = null);
