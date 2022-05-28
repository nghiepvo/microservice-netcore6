using Basket.API.Applications.Services;

namespace Basket.API.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IBasketService, BasketService>();

        return services;
    }
}
