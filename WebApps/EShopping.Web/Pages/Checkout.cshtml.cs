using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class CheckoutModel(IBasketService basket, ILogger<CheckoutModel> logger) : PageModel
    {
        public ShoppingCartModel Cart { get; set; }

        [BindProperty]
        public BasketChekoutModel Order { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await basket.GetOrCreateBasket("test");

            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            Cart = await basket.GetOrCreateBasket("test");


            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.CustomerId = new Guid("58c49479-ec65-4de2-86e7-033c546291aa");
            Order.UserName = "test";
            Order.TotalPrice = Cart.TotalPrice;

            var res = await basket.CheckoutBasket(new CheckoutBasketRequest(Order));

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
