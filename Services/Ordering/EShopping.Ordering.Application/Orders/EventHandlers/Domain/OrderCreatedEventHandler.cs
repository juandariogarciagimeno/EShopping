using MassTransit;
using Microsoft.FeatureManagement;

namespace EShopping.Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint, IFeatureManager featureManager) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            logger.LogInformation("Domain event {DomainEvent} handled", notification.GetType().Name);

            var orderCreatedIntegrationEvent = notification.Order.ToOrderDto();
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}
