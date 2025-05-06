namespace EShopping.Web.Models.Basket;

public class ShoppingCartModel
{
    public string UserName { get; set; } = null!;
    public List<ShoppingCartItemModel> Items { get; set; } = [];
    public decimal TotalPrice { get; set; }
}

public class ShoppingCartItemModel
{
    public int Quantity { get; set; }
    public string Color { get; set; } = null!;
    public decimal Price { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
}


public record GetBasketResponse(ShoppingCartModel Cart);

public record StoreBasketRequest(ShoppingCartModel Cart);
public record StoreBasketResponse(string UserName);

public record DeleteBasketResponse(bool IsSuccess);