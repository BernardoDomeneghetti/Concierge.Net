using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class UpdateResidentCommand : ICommand
{
    public Guid ResidentId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string MobilePhone { get; init; } = string.Empty;
    public string? PhotoUrl { get; init; }
    public Guid UnitId { get; init; }
    public ResidentType ResidentType { get; init; }
    public NotificationChannel NotificationChannel { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
}
