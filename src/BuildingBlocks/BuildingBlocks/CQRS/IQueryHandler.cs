using MediatR;

namespace BuildingBlocks.CQRS
{
    /// <summary>
    /// Used for queries that return a response. 
    /// TQuery: The type of the query.
    ///	TResponse: The type of the response returned by the query.
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
        where TResponse : notnull
    {

    }
}
