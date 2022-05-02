using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.Test.Extensions.Odata;

/// <summary>
/// The startup base class
/// </summary>
public class TestStartupBase
{
    public virtual void ConfigureServices(IServiceCollection services)
    {
    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        ConfigureBeforeRouting(app, env);

        app.UseRouting();

        ConfigureInRouting(app, env);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    protected virtual void ConfigureBeforeRouting(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

    protected virtual void ConfigureInRouting(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }
}
