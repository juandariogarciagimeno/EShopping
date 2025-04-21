using EShopping.Catalog.Data.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID is required")
                .NotEqual(Guid.Empty).WithMessage("Product ID is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class UpdateProductHandler(IProductRepository repo, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var original = await repo.GetById(request.Id, cancellationToken) ?? throw NotFoundException.Product(request.Id);


            var product = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                Price = request.Price,
                ImageFile = request.ImageFile
            };

            var updatedProduct = await repo.Update(product, cancellationToken);

            if (updatedProduct == null)
            {
                logger.LogError("Failed to update product with id: {Id}", request.Id);
                throw new Exception("Failed to update product");
            }

            return new UpdateProductResult(true);
        }
    }
}
