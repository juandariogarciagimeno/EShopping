namespace EShopping.Discount.Data.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount { get; set; }

        public static Coupon Empty => new Coupon() { Id = 0, Amount = 0, Description = "No Discount", ProductName = "No Discount" };
    }
}
