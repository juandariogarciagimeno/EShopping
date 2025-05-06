using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class OrderListModel(IOrderingService orders, ILogger<OrderListModel> logger) : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Orders = (await orders.GetOrdersByCustomer(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")))?.Orders;

            return Page();
        }
    }
}
