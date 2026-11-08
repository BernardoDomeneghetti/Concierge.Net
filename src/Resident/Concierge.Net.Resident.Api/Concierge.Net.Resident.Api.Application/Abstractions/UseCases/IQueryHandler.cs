using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Resident.Api.Application.Abstractions.UseCases;

public interface IQueryHandler<TQuery, TResponse> where TQuery : IQuery
{
    Task<TResponse> ExecuteAsync(TQuery query, CancellationToken correlationToken);
}
