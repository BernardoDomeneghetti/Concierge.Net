using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Packages.Commands;

public class CollectPackageCommand : ICommand
{
    public Guid PackageId { get; init; }
    public Guid CollectedByResidentId { get; init; }
    public string SignatureHash { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime? CollectedAtUtc { get; init; }
}

