using Concierge.Net.Application.Abstractions.Messaging;

namespace Concierge.Net.Application.Abstractions.UseCases;

public interface IQueryHandler <TQuery, TResponse> where TQuery: IQuery
{
    Task<TResponse> ExecuteAsync(TQuery Query, CancellationToken correlationToken);
}
