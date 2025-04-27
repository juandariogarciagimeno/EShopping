

using EShopping.Basket.Data.Interfaces;

namespace EShopping.Basket.Features.Basket.DeleteBasket;

public class DeleteBasketValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.");
    }
}

public class DeleteBasketHandler(IBasketRepository repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await repo.DeleteBasket(request.UserName, cancellationToken);
        return new DeleteBasketResult(result);
    }
}