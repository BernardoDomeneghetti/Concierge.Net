using Concierge.Net.Resident.Domain.Residents;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Services;

public sealed class FakeSmsService : ISmsService
{
    public Task SendAsync(
        PhoneNumber to,
        string message,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("=== SMS SIMULADO ===");
        Console.WriteLine($"Para: {to}");
        Console.WriteLine($"Mensagem: {message}");
        Console.WriteLine("====================");

        return Task.CompletedTask;
    }
}
