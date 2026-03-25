using System;
using Concierge.Net.Application.Abstractions.Messaging;

namespace Concierge.Net.Application.Package.Commands;

public class ReceivePackageCommand: ICommand
{
    public Guid ResidentId { get; init; }
    public Guid UnitId { get; init; }
    public string Email { get; init; } = string.Empty;
}
