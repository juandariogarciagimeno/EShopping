using EShopping.Ordering.Domain.Enums;

namespace EShopping.Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    IEnumerable<OrderItemDto> OrderItems
);
