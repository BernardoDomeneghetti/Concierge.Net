using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;
using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Residents.Commands;

public class RegisterResidentCommand : ICommand
{
    public string FullName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string MobilePhone { get; init; } = string.Empty;
    public string? PhotoUrl { get; init; }
    public Guid UnitId { get; init; }
    public ResidentType ResidentType { get; init; } = ResidentType.Owner;
    public NotificationChannel NotificationChannel { get; init; } = NotificationChannel.Email;
}
