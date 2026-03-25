using System;
using Concierge.Net.Application.Abstractions.Messaging;
using Concierge.Net.Application.Abstractions.UseCases;
using Microsoft.Extensions.Logging;

namespace Concierge.Net.Application.Decorators;

public sealed class QueryLoggingDecorator<TQuery, TResponse> 
    : IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery
{
    private readonly IQueryHandler<TQuery, TResponse> _inner;
    private readonly ILogger<QueryLoggingDecorator<TQuery, TResponse>> _logger;

    public QueryLoggingDecorator(
        IQueryHandler<TQuery, TResponse> inner,
        ILogger<QueryLoggingDecorator<TQuery, TResponse>> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<TResponse> ExecuteAsync(
        TQuery request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Query executed: {Query}", typeof(TQuery).Name);

        return await _inner.ExecuteAsync(request, cancellationToken);
    }
}