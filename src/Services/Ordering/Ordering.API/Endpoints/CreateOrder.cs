using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints
{
    //- Accepts a CreateOrderRequest object.
    //- Maps the request to a CreateOrderCommand.
    //- Uses MediatR to send the command to the corresponding handler.
    //- Returns a response with the created order's ID.

    /// <summary>
    /// CreateOrderRequest is a record that represents the request to create an order.
    /// </summary>
    /// <param name="Order"></param>
    public record CreateOrderRequest(OrderDto Order);

    /// <summary>
    /// CreateOrderResponse is a record that represents the response after creating an order.
    /// It is used to format the response that is sent back to the client. It is part of the API layer 
    /// and is designed to represent the data in a way that is suitable for the client.
    /// By using a separate response type, you can easily change the structure of the API response 
    /// without affecting the internal command handling logic. This provides flexibility in how you present data to the client.
    /// </summary>
    /// <param name="Id"></param>
    public record CreateOrderResponse(Guid Id);

    /// <summary>
    /// The API endpoint (CreateOrder) focuses on handling HTTP requests and responses, 
    /// converting the CreateOrderResult to a CreateOrderResponse before sending it back to the client.
    /// </summary>
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

                // Dependency Injection: When the sender.Send(command) method is called, MediatR uses the DI container to resolve the appropriate handler
                // for the command based on the command type.
                // When sender.Send(command) is called, MediatR resolves the CreateOrderHandler based on the type of the CreateOrderCommand.
                var result = await sender.Send(command);

                // Convert the CreateOrderResult to a CreateOrderResponse before sending it back to the client.
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
