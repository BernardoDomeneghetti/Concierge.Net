using System;
using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Packages.Commands;

public class ReceivePackageCommand: ICommand
{
    public Guid ResidentId { get; init; }
    public Guid UnitId { get; init; }
    public string Email { get; init; } = string.Empty;
}
