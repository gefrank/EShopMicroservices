

using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Used for commands that do not return a response.
    /// designed to handle commands, which are requests to perform an action that changes the state of the system
    /// TCommand: The type of the command.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, Unit>
        where TCommand : ICommand
    {
    }


    /// <summary>
    /// Used for commands that return a response.
    /// designed to handle commands, which are requests to perform an action that changes the state of the system
    /// </summary>
    /// TCommand: The type of the command.
    ///	TResponse: The type of the response returned by the command
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
        where TResponse : notnull
    {
    }    
}
