using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class ProductDetailModel(ICatalogService catalog, IBasketService basket, ILogger<ProductDetailModel> logger) : PageModel
    {
        public ProductModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid productId)
        {
            Product = (await catalog.GetProductById(productId))?.Product;

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
