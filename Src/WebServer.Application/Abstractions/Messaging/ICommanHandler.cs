namespace WebServer.Application.Abstractions.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<ICommand, TResponse>
    : IRequestHandler<ICommand, Result<TResponse>>
    where ICommand : ICommand<TResponse>
{ 
}
