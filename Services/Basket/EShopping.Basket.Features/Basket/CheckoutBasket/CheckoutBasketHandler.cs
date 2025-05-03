
using EShopping.Basket.Data.Interfaces;
using EShopping.Shared.BuildingBlocks.Messaging.Events;
using MassTransit;

namespace EShopping.Basket.Features.Basket.CheckoutBasket;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.Checkout)
            .NotNull()
            .WithMessage("Basket Checkout required.");

        RuleFor(x => x.Checkout.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");
    }
}

public class CheckoutBasketHandler(IBasketRepository repo, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repo.GetBasket(command.Checkout.UserName, cancellationToken);
        if (basket is null)
            return new CheckoutBasketResult(false);

        var eventMessage = command.Checkout.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        eventMessage.Items = basket.Items.Select(x => new BasketItem
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity,
            Price = x.Price
        }).ToList();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repo.DeleteBasket(command.Checkout.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
