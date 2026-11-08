using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Packages.Commands;

public class CancelPackageCommand : ICommand
{
    public Guid PackageId { get; init; }
    public string Reason { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime? CancelledAtUtc { get; init; }
}

