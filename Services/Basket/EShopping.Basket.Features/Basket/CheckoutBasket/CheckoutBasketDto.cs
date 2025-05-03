namespace EShopping.Basket.Features.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto Checkout) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public record CheckoutBasketRequest(CheckoutBasketDto Checkout);
public record CheckoutBasketResponse(bool IsSuccess);

public record CheckoutBasketDto()
{
    public string UserName { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; }

    // Shipping and Billing Addresses
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string EmailAddress { get; set; } = null!;
    public string AddressLine { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;

    // Payment
    public string CardName { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public string Expiration { get; set; } = null!;
    public string Cvv { get; set; } = null!;
    public int PaymentMethod { get; set; }
}