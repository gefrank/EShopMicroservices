
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponse(Guid Id);

    /// <summary>
    /// The ICarterModule is available because we added MapCarter to the service collection in Program.cs
    /// </summary>
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                // Adapt is a Mapster method that converts one object to another
                var command = request.Adapt<CreateProductCommand>();

                // Send the command to the mediator, which will handle it
                var result = await sender.Send(command);

                // Adapt the result to the response object, this is also a Mapster method
                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct") 
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}
