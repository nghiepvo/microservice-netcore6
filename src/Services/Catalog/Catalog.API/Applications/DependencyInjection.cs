using Common.Libraries.API.Applications.Services;

namespace Common.Libraries.API.Applications;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}