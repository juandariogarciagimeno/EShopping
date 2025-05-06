using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class CartModel(IBasketService basket, ILogger<CartModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basket.GetOrCreateBasket("test");
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
        {
            logger.LogInformation("Remove cart item button cliked");

            var cart = await basket.GetOrCreateBasket("test");
            cart.Items.RemoveAll(i => i.ProductId == productId);

            return RedirectToPage();
        }
    }
}
