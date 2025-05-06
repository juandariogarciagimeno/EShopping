using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class ProductListModel(ICatalogService catalog, IBasketService basket, ILogger<ProductListModel> logger) : PageModel
    {

        public IEnumerable<ProductModel> ProductList { get; set; }
        public IEnumerable<string> CategoryList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            var allProducts = (await catalog.GetProducts()).Products;
            CategoryList = allProducts.SelectMany(p => p.Category).Distinct();

            if (string.IsNullOrEmpty(categoryName))
            {
                ProductList = (await catalog.GetProductsByCategory(categoryName)).Products;
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = allProducts;
            }

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
