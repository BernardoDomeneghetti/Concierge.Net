using Concierge.Net.Resident.Domain.Residents;

namespace Concierge.Net.Resident.Api.Contracts;

public sealed record RegisterResidentRequest(
    string FullName,
    string Cpf,
    string Email,
    string MobilePhone,
    string? PhotoUrl,
    Guid UnitId,
    ResidentType ResidentType = ResidentType.Owner,
    NotificationChannel NotificationChannel = NotificationChannel.Email);

public sealed record UpdateResidentRequest(
    string FullName,
    string Email,
    string MobilePhone,
    string? PhotoUrl,
    Guid UnitId,
    ResidentType ResidentType = ResidentType.Owner,
    NotificationChannel NotificationChannel = NotificationChannel.Email,
    DateTime? UpdatedAtUtc = null);

public sealed record InactivateResidentRequest(
    string Reason,
    DateTime? InactivatedAtUtc = null);
