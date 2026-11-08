using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class InactivateResidentCommand : ICommand
{
    public Guid ResidentId { get; init; }
    public string Reason { get; init; } = string.Empty;
    public DateTime? InactivatedAtUtc { get; init; }
}
