
using EShopping.Basket.Data.Interfaces;
using EShopping.Discount.Grpc;
using static EShopping.Discount.Grpc.Discount;

namespace EShopping.Basket.Features.Basket.StoreBasket;

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketHandler(IBasketRepository repo, DiscountClient discount) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.Cart;

        await ApplyDiscounts(cart, cancellationToken);

        var result = (await repo.AddOrUpdateBasket(cart, cancellationToken)) ?? throw new Exception("Error storing basket");
        return new StoreBasketResult(result.UserName);
    }

    private async Task ApplyDiscounts(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var discountResponse = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= discountResponse?.Amount ?? 0;
        }
    }
}
