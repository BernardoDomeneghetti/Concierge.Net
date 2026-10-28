using Concierge.Net.Application.Abstractions.Context;
using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.UseCases;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Application.Decorators;

public sealed class QueryHandlerLoggingDecorator<TQuery, TResponse> 
    : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery
{
    private readonly IQueryHandler<TQuery, TResponse> _inner;
    private readonly ILogger<QueryHandlerLoggingDecorator<TQuery, TResponse>> _logger;
    private readonly ICorrelationContext _context;

    public QueryHandlerLoggingDecorator(
        IQueryHandler<TQuery, TResponse> inner,
        ILogger<QueryHandlerLoggingDecorator<TQuery, TResponse>> logger,
        ICorrelationContext context)
    {
        _inner = inner;
        _logger = logger;
        _context = context;
    }

    public async Task<TResponse> ExecuteAsync(
        TQuery request,
        CancellationToken cancellationToken = default)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = _context.CorrelationId,
            ["Type"] = "Query",
            ["Request"] = typeof(TQuery).Name
        }))
        {
            try
            {
                _logger.LogInformation("Query started");

                var result = await _inner.ExecuteAsync(request, cancellationToken);

                _logger.LogInformation("Query completed");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing query");
                throw;
            }
        }
    }
}
