
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);

    /// <summary>
    /// The ICarterModule is available because we added MapCarter to the service collection in Program.cs
    /// </summary>
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                // Adapt is a Mapster method that converts one object to another
                var command = request.Adapt<UpdateProductCommand>();

                // Send the command to the mediator, which will handle it
                var result = await sender.Send(command);

                // Adapt the result to the response object, this is also a Mapster method
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct") 
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
