using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Domain.Residents;

public class ResidentRegisteredDomainEvent(
    ResidentId residentId,
    string fullName,
    Cpf cpf,
    Email email,
    PhoneNumber mobilePhone,
    UnitId unitId,
    ResidentType residentType,
    NotificationChannel notificationChannel) : DomainEvent
{
    public ResidentId ResidentId { get; } = residentId;
    public string FullName { get; } = fullName;
    public Cpf Cpf { get; } = cpf;
    public Email Email { get; } = email;
    public PhoneNumber MobilePhone { get; } = mobilePhone;
    public UnitId UnitId { get; } = unitId;
    public ResidentType ResidentType { get; } = residentType;
    public NotificationChannel NotificationChannel { get; } = notificationChannel;
}
