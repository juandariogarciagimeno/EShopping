namespace EShopping.Web.Services;

public interface IBasketService
{
    [Get("/basket/{userName}")]
    Task<GetBasketResponse> GetBasket(string userName);

    [Post("/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket/{userName}")]
    Task<DeleteBasketResponse> DeleteBasket(string userName);

    [Post("/basket/checkout")]
    Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

    public async Task<ShoppingCartModel> GetOrCreateBasket(string userName)
    {
        ShoppingCartModel basket;
        try
        {
            basket = (await GetBasket(userName)).Cart;  
        } 
        catch (ApiException apiEx) when (apiEx.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            basket = new ShoppingCartModel()
            {
                UserName = userName,
                Items = []
            };
        }

        return basket;
    }
}
