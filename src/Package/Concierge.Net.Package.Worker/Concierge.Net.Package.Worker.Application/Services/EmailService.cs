using System;
using Concierge.Net.Package.Worker.Application.Abstractions.Services;
using Concierge.Net.Package.Domain.Shared;

namespace Concierge.Net.Package.Worker.Application.Services;

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
