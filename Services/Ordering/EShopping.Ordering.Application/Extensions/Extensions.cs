namespace EShopping.Ordering.Application.Extensions;

public static class Extensions
{
    public static Address ToAddress (this AddressDto dto)
    {
        return Address.Of(
            dto.FirstName,
            dto.LastName,
            dto.EmailAddress,
            dto.AddressLine,
            dto.Country,
            dto.State,
            dto.ZipCode);
    }

    public static Payment ToPayment(this PaymentDto dto)
    {
        return Payment.Of(
            dto.CardName,
            dto.CardNumber,
            dto.Expiration,
            dto.Cvv,
            dto.PaymentMethod);
    }

    public static AddressDto ToAddressDto(this Address dto)
    {
        return new AddressDto(
            dto.FirstName,
            dto.LastName,
            dto.EmailAddress,
            dto.AddressLine,
            dto.Country,
            dto.State,
            dto.ZipCode);
    }

    public static PaymentDto ToPaymentDto(this Payment dto)
    {
        return new PaymentDto(
            dto.CardName,
            dto.CardNumber,
            dto.Expiration,
            dto.CVV,
            dto.PaymentMethod);
    }

    public static OrderDto ToOrderDto(this Order dto)
    {
        return new OrderDto(
            dto.Id,
            dto.CustomerId,
            dto.OrderName,
            dto.ShippingAdress.ToAddressDto(),
            dto.BillingAdress.ToAddressDto(),
            dto.Payment.ToPaymentDto(),
            dto.Status,
            dto.OrderItems.Select(x => new OrderItemDto(x.OrderId, x.ProductId, x.Quantity, x.Price)));
    }

    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(x => x.ToOrderDto());
    }
}
