namespace EShopping.Basket.Features.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);