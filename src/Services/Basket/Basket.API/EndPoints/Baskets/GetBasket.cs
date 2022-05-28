using Basket.API.Applications.Services;
using Basket.API.Commons;
using Basket.API.Domian;

namespace Basket.API.EndPoints.Baskets;
public class GetBasket : Endpoint<TypeRequest<string>, ShoppingCart>
{
    private readonly IBasketService _basketService;

    public GetBasket(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public override void Configure()
    {
        Get(BasketRoutes.GetBasket);
        Version(1);
        Permissions(Allow.BasketRead);
    }

    public override async Task HandleAsync(TypeRequest<string> req, CancellationToken ct)
        => await SendAsync(_basketService.GetBasket(req.Payload), cancellation: ct);
}