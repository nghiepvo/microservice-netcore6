using Basket.API.Domian;
using Redis.OM;
using static Basket.API.Infrastructures.Redis.Cache;

namespace Basket.API.Applications.Services;

public interface IBasketService
{
    ShoppingCart GetBasket(string userName);
    ShoppingCart UpdateBasket(ShoppingCart basket);
    void DeleteBasket(string userName);
}

public class BasketService : IBasketService
{
    public void DeleteBasket(string userName)
    {
        Context.Execute("JSON.DEL", userName);
    }

    public ShoppingCart GetBasket(string userName)
    {
        var shoppingCart = Context.JsonGet<ShoppingCart>(userName??"");

        if (shoppingCart == null)
        {
            shoppingCart = new ShoppingCart(userName??"");
        }

        return shoppingCart;
    }

    public ShoppingCart UpdateBasket(ShoppingCart basket)
    {
        Context.JsonSet(basket.UserName, ".",  basket);

        return GetBasket(basket.UserName);
    }
}
