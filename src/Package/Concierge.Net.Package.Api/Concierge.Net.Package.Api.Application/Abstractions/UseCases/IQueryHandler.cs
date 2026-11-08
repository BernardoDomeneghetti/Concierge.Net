using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Abstractions.UseCases;

public interface IQueryHandler <TQuery, TResponse> where TQuery: IQuery
{
    Task<TResponse> ExecuteAsync(TQuery Query, CancellationToken correlationToken);
}
