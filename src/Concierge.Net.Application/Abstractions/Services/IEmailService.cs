using System;
using Concierge.Net.Domain.Residents;

namespace Concierge.Net.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default);
}
