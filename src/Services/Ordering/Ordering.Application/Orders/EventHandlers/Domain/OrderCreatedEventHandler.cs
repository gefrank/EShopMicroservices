using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
                            : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

            // Only publish the event if the feature is enabled, otherwise it will be handled by the OrderUpdatedEventHandler,
            // such as seeding the database with test data.
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
                // Publish the integration event to rabbitmq
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        

        }
    }

}
