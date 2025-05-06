using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EShopping.Web.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
        }

        public void OnGetOrderSubmitted()
        {
            Message = "Your order has been submitted successfully. Thank you for shopping with us!";
        }
    }
}
