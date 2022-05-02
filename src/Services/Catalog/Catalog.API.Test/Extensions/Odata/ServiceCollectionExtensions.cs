using System;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Test.Extensions.Odata;

/// <summary>
/// Extension for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Config the controller provider.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="controllers">The configured controllers.</param>
    /// <returns>The caller.</returns>
    public static IServiceCollection ConfigureControllers(this IServiceCollection services, params Type[] controllers)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddControllers()
            .ConfigureApplicationPartManager(pm =>
            {
                pm.FeatureProviders.Add(new WebODataControllerFeatureProvider(controllers));
            });

        return services;
    }
}
