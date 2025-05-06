﻿namespace EShopping.Web.Models.Ordering;

public record OrderModel(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressModel ShippingAddress,
    AddressModel BillingAddress,
    PaymentModel Payment,
    OrderStatus Status,
    IEnumerable<OrderItemModel> OrderItems
);
public record OrderItemModel(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price
    );


public record AddressModel(
    string FirstName,
    string LastName,
    string AddressLine,
    string EmailAddress,
    string Country,
    string State,
    string ZipCode
);

public record PaymentModel(
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    int PaymentMethod
);

public enum OrderStatus : int
{
    Draft = 1,
    Pending = 2,
    Completed = 3,
    Cancelled = 4
}

public record GetOrdersResponse(PaginatedResult<OrderModel> Orders);
public record GetOrdersByNameResponse(IEnumerable<OrderModel> Orders);
public record GetOrdersByCustomerResponse(IEnumerable<OrderModel> Orders);