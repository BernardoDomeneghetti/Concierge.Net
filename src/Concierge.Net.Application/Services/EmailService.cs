using System;
using Concierge.Net.Application.Abstractions.Services;
using Concierge.Net.Domain.Shared;

namespace Concierge.Net.Application.Services;

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
