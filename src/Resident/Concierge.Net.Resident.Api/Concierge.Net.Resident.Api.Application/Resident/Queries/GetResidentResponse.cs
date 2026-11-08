using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Application.Residents.Queries;

public class GetResidentResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Cpf { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string MobilePhone { get; init; } = string.Empty;
    public string? PhotoUrl { get; init; }
    public string? BiometricSignature { get; init; }
    public Guid UnitId { get; init; }
    public ResidentType ResidentType { get; init; }
    public NotificationChannel NotificationChannel { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public DateTime? InactivatedAtUtc { get; init; }
    public string? InactivationReason { get; init; }
    public string Status { get; init; } = string.Empty;
}
