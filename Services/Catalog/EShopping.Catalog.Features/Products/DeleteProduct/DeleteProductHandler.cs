
using EShopping.Catalog.Data.Interfaces;
using EShopping.Catalog.Features.Products.UpdateProduct;
using EShopping.Shared.BuildingBlocks.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<UpdateProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required")
            .NotEqual(Guid.Empty).WithMessage("Product ID is required");
    }
}

public class DeleteProductHandler(IProductRepository repo) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var p = await repo.GetById(request.Id) ?? throw NotFoundException.Product(request.Id);

        var result = await repo.Delete(p, cancellationToken);

        return new DeleteProductResult(result);
    }
}
