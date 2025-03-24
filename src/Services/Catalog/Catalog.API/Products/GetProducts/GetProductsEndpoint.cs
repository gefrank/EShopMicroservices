

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetProductsResponse(IEnumerable<Product> Products);

    // Using Carter to create an endpoint because it simplifies the process
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // ISender comes from mediator, it's a way to send messages to the mediator
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();

                // Behind the scenes, MediatR will:
                // Find the appropriate handler for the GetProductsQuery request
                // Pass the request to that handler
                // The handler processes the request(likely fetching products from a database)
                // Return the result back through the pipeline

                // The beauty of this approach is that your API endpoint doesn't need to know how products are
                // retrieved or where they come from. It simply asks for products and gets them, creating a
                // clean separation between your API layer and business logic.
                var result = await sender.Send(query);

                // Using mapster to map the result to the response object
                // mapster doen't require any further configuration here because the properties are the same
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK) 
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .WithDescription("Get all products");
        }
    }
}
