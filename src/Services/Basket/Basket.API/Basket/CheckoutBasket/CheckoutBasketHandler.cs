
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }

    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
                                            : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            // get existing basket with total price
            // Set totalprice on basketcheckout event message
            // send basket checkout event to rabbitmq using masstransit
            // delete the basket

            var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);
            if (basket == null)
            {
                return new CheckoutBasketResult(false);
            }

            var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            // This an example of a dual write problem. Publish and Delete are not in a transaction.
            // Need to use the Transactional Outbox pattern to solve this problem. 
            // In this case we are going to create an outbox table and publish the event from there.

            // publish checkout event to rabbitmq using masstransit
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
