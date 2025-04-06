using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    //- Accepts a CreateOrderRequest object.
    //- Maps the request to a CreateOrderCommand.
    //- Uses MediatR to send the command to the corresponding handler.
    //- Returns a response with the created order's ID.

    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // The code defines a POST endpoint at /orders that handles creating an order. When a request is received, it takes a CreateOrderRequest            
            // object and an ISender instance. The request object is adapted to a CreateOrderCommand using Mapster.The ISender is likely used to
            // send the command for further processing, although the actual sending logic is not shown in the provided snippet.
            // Dependency Injection: The ISender parameter is resolved by the DI container and injected into the endpoint handler.
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();

                var result = await sender.Send(command);    

                var response = new CreateOrderResponse(result.Id);

                return Results.Created($"/orders/{result.Id}", response);

            })
            .WithName("CreateOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");
        }
    }
}
