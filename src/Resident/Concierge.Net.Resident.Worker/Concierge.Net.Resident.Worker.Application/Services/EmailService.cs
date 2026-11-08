using Concierge.Net.Resident.Domain.Shared;
using Concierge.Net.Resident.Worker.Application.Abstractions.Services;

namespace Concierge.Net.Resident.Worker.Application.Services;

public sealed class FakeEmailService : IEmailService
{
    public Task SendAsync(
        Email to,
        string subject,
        string body,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine("=== EMAIL SIMULADO ===");
        Console.WriteLine($"Para: {to}");
        Console.WriteLine($"Assunto: {subject}");
        Console.WriteLine($"Mensagem: {body}");
        Console.WriteLine("======================");

        return Task.CompletedTask;
    }
}
