using Concierge.Net.Resident.Domain.Base;
using Concierge.Net.Resident.Domain.Shared;

namespace Concierge.Net.Resident.Domain.Residents;

public sealed class Resident : AggregateRoot<ResidentId>
{
    public string FullName { get; private set; }
    public Cpf Cpf { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber MobilePhone { get; private set; }
    public string? PhotoUrl { get; private set; }
    public string? BiometricSignature { get; private set; }
    public UnitId UnitId { get; private set; }
    public ResidentType ResidentType { get; private set; }
    public NotificationChannel NotificationChannel { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
    public DateTime? InactivatedAtUtc { get; private set; }
    public string? InactivationReason { get; private set; }

    public bool IsActive => !InactivatedAtUtc.HasValue;

    private Resident(
        ResidentId id,
        string fullName,
        Cpf cpf,
        Email email,
        PhoneNumber mobilePhone,
        string? photoUrl,
        UnitId unitId,
        ResidentType residentType,
        NotificationChannel notificationChannel)
        : base(id)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        FullName = fullName.Trim();
        Cpf = cpf;
        Email = email;
        MobilePhone = mobilePhone;
        PhotoUrl = photoUrl;
        UnitId = unitId;
        ResidentType = residentType;
        NotificationChannel = notificationChannel;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public static Resident Register(
        string fullName,
        Cpf cpf,
        Email email,
        PhoneNumber mobilePhone,
        string? photoUrl,
        UnitId unitId,
        ResidentType residentType,
        NotificationChannel notificationChannel)
    {
        var resident = new Resident(
            ResidentId.New(),
            fullName,
            cpf,
            email,
            mobilePhone,
            photoUrl,
            unitId,
            residentType,
            notificationChannel);

        resident.AddDomainEvent(new ResidentRegisteredDomainEvent(
            resident.Id,
            resident.FullName,
            resident.Cpf,
            resident.Email,
            resident.MobilePhone,
            resident.UnitId,
            resident.ResidentType,
            resident.NotificationChannel));

        return resident;
    }

    public void UpdateProfile(
        string fullName,
        Email email,
        PhoneNumber mobilePhone,
        string? photoUrl,
        UnitId unitId,
        ResidentType residentType,
        NotificationChannel notificationChannel,
        DateTime updatedAtUtc)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update an inactive resident.");

        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.", nameof(fullName));

        FullName = fullName.Trim();
        Email = email;
        MobilePhone = mobilePhone;
        PhotoUrl = photoUrl;
        UnitId = unitId;
        ResidentType = residentType;
        NotificationChannel = notificationChannel;
        UpdatedAtUtc = updatedAtUtc;

        AddDomainEvent(new ResidentUpdatedDomainEvent(
            Id,
            FullName,
            Email,
            MobilePhone,
            UnitId,
            ResidentType,
            NotificationChannel,
            updatedAtUtc));
    }

    public void Inactivate(string reason, DateTime inactivatedAtUtc)
    {
        if (!IsActive)
            throw new InvalidOperationException("Resident is already inactive.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Inactivation reason is required.", nameof(reason));

        InactivationReason = reason.Trim();
        InactivatedAtUtc = inactivatedAtUtc;

        AddDomainEvent(new ResidentInactivatedDomainEvent(
            Id,
            FullName,
            Email,
            MobilePhone,
            UnitId,
            InactivationReason,
            inactivatedAtUtc,
            NotificationChannel));
    }

    public void SetBiometricSignature(string biometricSignature)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot set biometric signature for an inactive resident.");

        if (string.IsNullOrWhiteSpace(biometricSignature))
            throw new ArgumentException("Biometric signature cannot be empty.", nameof(biometricSignature));

        BiometricSignature = biometricSignature.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
