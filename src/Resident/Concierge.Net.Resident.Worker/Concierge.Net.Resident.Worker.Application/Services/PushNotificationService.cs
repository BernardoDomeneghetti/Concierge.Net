using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Services;

public sealed class FakePushNotificationService : IPushNotificationService
{
    public Task SendAsync(
        ResidentId residentId,
        string title,
        string message,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("=== PUSH SIMULADO ===");
        Console.WriteLine($"Morador: {residentId}");
        Console.WriteLine($"Título: {title}");
        Console.WriteLine($"Mensagem: {message}");
        Console.WriteLine("=====================");

        return Task.CompletedTask;
    }
}
