
/// This code implements a CQRS (Command Query Responsibility Segregation) pattern, specifically the query part
namespace Catalog.API.Products.GetProducts
{
    /// <summary>
    /// A simple record that serves as a message to request products. It implements IQuery<GetProductsResult>,
    /// indicating this query returns a GetProductsResult.
    /// </summary>
    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

    /// <summary>
    /// A record containing the query result - a collection of Product objects.
    /// </summary>
    /// <param name="Products"></param>
    public record GetProductsResult(IEnumerable<Product> Products);

    /// <summary>
    /// The handler class that processes the query. 
    /// It:
    /// Takes dependencies through constructor injection(session and logger)
    /// Implements IQueryHandler<GetProductsQuery, GetProductsResult>
    /// Contains the Handle method that executes the actual query logic
    /// </summary>
    /// <param name="session"></param>
    /// <param name="logger"></param>
    internal class GetProductQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {  
            // fetches all products from the database using session.Query...
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            // packages products into the result container and returns
            return new GetProductsResult(products); 
        }
    }
}
