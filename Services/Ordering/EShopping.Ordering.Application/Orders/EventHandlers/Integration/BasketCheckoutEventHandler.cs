using EShopping.Ordering.Application.Orders.Commands.CreateOrder;
using EShopping.Ordering.Domain.Enums;
using EShopping.Shared.BuildingBlocks.Messaging.Events;
using MassTransit;

namespace EShopping.Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = MapToCreateOrderCommand(context.Message);
        CreateOrderResult? result = null;
        try
        {
            result = await sender.Send(command, context.CancellationToken);
            if (result is null)
            {
                logger.LogError("Order creation failed");
                return;
            }

            logger.LogInformation("Order {id} created successfully", result.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when creating order");
        }
    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent dto)
    {
        var addressDto = new AddressDto(dto.FirstName, dto.LastName, dto.AddressLine, dto.EmailAddress, dto.Country, dto.State, dto.ZipCode);
        var paymentDto = new PaymentDto(dto.CardName, dto.CardNumber, dto.Expiration, dto.Cvv, dto.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderItemDtos = dto.Items.Select(x => new OrderItemDto(orderId, x.ProductId, x.Quantity, x.Price)).ToList();

        var orderDto = new OrderDto(
            orderId,
            dto.CustomerId,
            dto.UserName,
            addressDto,
            addressDto,
            paymentDto,
            OrderStatus.Pending,
            orderItemDtos
            );

        return new CreateOrderCommand(orderDto);
    }
}
