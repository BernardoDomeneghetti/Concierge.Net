using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Domain.Shared;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Services;

public sealed class ResidentNotificationService(
    IEmailService emailService,
    ISmsService smsService,
    IPushNotificationService pushNotificationService) : IResidentNotificationService
{
    private readonly IEmailService _emailService = emailService;
    private readonly ISmsService _smsService = smsService;
    private readonly IPushNotificationService _pushNotificationService = pushNotificationService;

    public async Task NotifyAsync(
        ResidentId residentId,
        Email email,
        PhoneNumber mobilePhone,
        NotificationChannel preferredChannels,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        if (preferredChannels.HasFlag(NotificationChannel.Email))
        {
            await _emailService.SendAsync(email, subject, message, cancellationToken);
        }

        if (preferredChannels.HasFlag(NotificationChannel.Sms))
        {
            await _smsService.SendAsync(mobilePhone, $"{subject}: {message}", cancellationToken);
        }

        if (preferredChannels.HasFlag(NotificationChannel.Push))
        {
            await _pushNotificationService.SendAsync(residentId, subject, message, cancellationToken);
        }
    }
}
