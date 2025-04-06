using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    /// <summary>
    /// The CreateOrderCommand is defined as a record that implements ICommand<CreateOrderResult>
    /// </summary>
    /// <param name="Order"></param>
    public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

    /// <summary>
    /// CreateOrderResult is used internally within the application to represent the result of the command execution.
    /// It is part of the command handling logic and is returned by the command handler.
    /// </summary>
    /// <param name="Id"></param>
    public record CreateOrderResult(Guid Id);
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
}
