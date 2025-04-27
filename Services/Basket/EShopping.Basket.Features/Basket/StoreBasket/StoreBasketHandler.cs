
using EShopping.Basket.Data.Interfaces;

namespace EShopping.Basket.Features.Basket.StoreBasket;

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketHandler(IBasketRepository repo) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;
        var result = (await repo.AddOrUpdateBasket(cart, cancellationToken)) ?? throw new Exception("Error storing basket");

        return new StoreBasketResult(result.UserName);
    }
}
