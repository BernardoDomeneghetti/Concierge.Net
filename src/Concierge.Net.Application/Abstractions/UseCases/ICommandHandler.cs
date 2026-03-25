using Concierge.Net.Application.Abstractions.Messaging;

namespace Concierge.Net.Application.Abstractions.UseCases;

public interface ICommandHandler <TCommand, TResponse> where TCommand: ICommand
{
    Task<TResponse> ExecuteAsync(TCommand command, CancellationToken correlationToken);
}
