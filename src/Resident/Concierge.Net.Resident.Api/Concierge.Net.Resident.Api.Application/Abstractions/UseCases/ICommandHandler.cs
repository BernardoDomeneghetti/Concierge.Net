using Concierge.Net.Resident.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Resident.Api.Application.Abstractions.UseCases;

public interface ICommandHandler<TCommand, TResponse> where TCommand : ICommand
{
    Task<TResponse> ExecuteAsync(TCommand command, CancellationToken correlationToken);
}
