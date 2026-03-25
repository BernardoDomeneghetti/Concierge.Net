using System;
using Concierge.Net.Application.Abstractions.Context;
using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.UseCases;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Application.Decorators;

public sealed class CommandHandlerLoggingDecorator<TCommand, TResponse> 
    : ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand, TResponse> _inner;
    private readonly ILogger<CommandHandlerLoggingDecorator<TCommand, TResponse>> _logger;
    private readonly ICorrelationContext _context;

    public CommandHandlerLoggingDecorator(
        ICommandHandler<TCommand, TResponse> inner,
        ILogger<CommandHandlerLoggingDecorator<TCommand, TResponse>> logger,
        ICorrelationContext context)
    {
        _inner = inner;
        _logger = logger;
        _context = context;
    }

    public async Task<TResponse> ExecuteAsync(
        TCommand command,
        CancellationToken cancellationToken = default)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = _context.CorrelationId,
            ["Type"] = "Command",
            ["Request"] = typeof(TCommand).Name
        }))
        {
            _logger.LogInformation("Command started");

            var result = await _inner.ExecuteAsync(command, cancellationToken);

            _logger.LogInformation("Command completed");

            return result;
        }
    }
}
