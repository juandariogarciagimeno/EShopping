namespace EShopping.Basket.Data.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; } = null!;
        public List<ShoppingCartItem> Items { get; set; } = [];
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity); 

        public static ShoppingCart FromUserName(string username)
        {
            return new ShoppingCart
            {
                UserName = username,
                Items = []
            };
        }
    }
}
