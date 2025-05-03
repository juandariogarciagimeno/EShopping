namespace EShopping.Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);



public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .WithMessage("Order cannot be null");

        RuleFor(x => x.Order.CustomerId)
            .NotNull()
            .WithMessage("CustomerId cannot be empty");

        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("OrderName cannot be empty");

        RuleFor(x => x.Order.OrderItems)
            .NotEmpty()
            .WithMessage("OrderItems cannot be empty");
    }
}