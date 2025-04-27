namespace EShopping.Basket.Features.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public record GetBasketResponse(ShoppingCart Cart);