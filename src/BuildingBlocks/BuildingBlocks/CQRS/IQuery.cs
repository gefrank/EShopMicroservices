using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Used for queries that return a response.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull   
    {        
    }
}
