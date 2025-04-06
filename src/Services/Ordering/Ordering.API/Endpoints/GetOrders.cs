using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

//- Accepts pagination parameters.
//- Constructs a GetOrdersQuery with these parameters.
//- Retrieves the data and returns it in a paginated format.

//public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // [AsParameters]: This attribute is used to indicate that the parameters of the method should be bound from the query string or route values.
        // It simplifies the binding of multiple parameters from the request. i.e. /orders?pageIndex=2&pageSize=20
        //
        // If you didn't use [AsParameters], you would have to manually extract the parameters from the request.
        // Manually extract query parameters like this:
        // var pageIndex = int.TryParse(context.Request.Query["pageIndex"], out var pi) ? pi : 1;
        // var pageSize = int.TryParse(context.Request.Query["pageSize"], out var ps) ? ps : 10;
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(request));

            var response = result.Adapt<GetOrdersResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}
