using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class IndexModel(ICatalogService catalog, IBasketService basket, ILogger<IndexModel> logger) : PageModel
    {
        public IEnumerable<ProductModel> ProductList { get; set; } = [];

        public async Task<IActionResult> OnGetAsync()
        {
            logger.LogInformation("Index page Visited");

            var result = await catalog.GetProducts(2,3);
            ProductList = result.Products;

            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            var cart = await basket.GetOrCreateBasket("test");
            var product = (await catalog.GetProductById(productId))?.Product;

            if (product is null)
            {
                return NotFound();
            }

            cart.Items.Add(new ShoppingCartItemModel()
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
            });

            await basket.StoreBasket(new StoreBasketRequest(cart));

            return RedirectToPage("Cart");
        }
    }
}
