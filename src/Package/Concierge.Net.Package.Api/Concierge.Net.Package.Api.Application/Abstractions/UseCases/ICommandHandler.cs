using Concierge.Net.Package.Api.Application.Abstractions.Messaging;

namespace Concierge.Net.Package.Api.Application.Abstractions.UseCases;

public interface ICommandHandler <TCommand, TResponse> where TCommand: ICommand
{
    Task<TResponse> ExecuteAsync(TCommand command, CancellationToken correlationToken);
}
