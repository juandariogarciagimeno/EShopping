
namespace EShopping.Basket.Features.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
public record DeleteBasketResult(bool Success);

public record DeleteBasketResponse(bool Success);