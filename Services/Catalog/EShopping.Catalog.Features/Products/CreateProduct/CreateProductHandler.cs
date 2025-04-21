using EShopping.Catalog.Data.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace EShopping.Catalog.Features.Products.CreateProduct;

public class CreateProductHandler(IProductRepository repo, ILogger<CreateProductHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        }
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        logger.LogInformation("Creating product with name: {Name}", request.Name);
        //Create Product Entity from command object
        var product = new Product
        {
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            Price = request.Price,
            ImageFile = request.ImageFile
        };

        //Save Product Entity to database
        product = await repo.Add(product, cancellationToken);

        if (product == null)
        {
            logger.LogError("Failed to create product with name: {Name}", request.Name);
            throw new Exception("Failed to create product");
        }

        //Return the result with the created product ID
        return new CreateProductResult(product.Id);
    }
}
