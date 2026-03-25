using System;
using Concierge.Net.Application.Abstractions.Context;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Application.Decorators;

public sealed class UseCaseLoggingDecorator<TUseCase>
{
    private readonly TUseCase _inner;
    private readonly ILogger<TUseCase> _logger;
    private readonly ICorrelationContext _context;

    public UseCaseLoggingDecorator(
        TUseCase inner,
        ILogger<TUseCase> logger,
        ICorrelationContext context)
    {
        _inner = inner;
        _logger = logger;
        _context = context;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        var correlationId = _context.CorrelationId;

        using (_logger.BeginScope(new { CorrelationId = correlationId }))
        {
            var start = DateTime.UtcNow;

            try
            {
                _logger.LogInformation("Use case started");

                await action();

                var duration = DateTime.UtcNow - start;

                _logger.LogInformation(
                    "Use case completed in {Duration}ms",
                    duration.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Use case failed");
                throw;
            }
        }
    }
}
