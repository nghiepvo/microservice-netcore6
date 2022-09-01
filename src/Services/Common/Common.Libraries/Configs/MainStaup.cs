using Common.Libraries.EndPoints;
using Common.Libraries.Nswag;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;

namespace Common.Libraries.Configs;

public static class MainStaup
{
    public static void DefaultBuilder(this WebApplicationBuilder builder, string apiTitle, Action<WebApplicationBuilder>? moreConfig = null)
    {
        builder.Services.AddFastEndpoints();
        builder.Services.AddAuthenticationJWTBearer(builder.Configuration[EndPointConfig.TokenKey]);
        builder.Services.AddSwaggerDoc(settings =>
        {
            settings.DocumentName = EndPointConfig.Version2Str;
            settings.Title = apiTitle;
            settings.Version = EndPointConfig.Version2Str;
            settings.AddAuthController();
            settings.OperationProcessors.Add(new AddOdataQuery());
        }, maxEndpointVersion: EndPointConfig.Version2, addJWTBearerAuth: false)
        .AddSwaggerDoc(settings =>
        {
            settings.DocumentName = EndPointConfig.Version1Str;
            settings.Title = apiTitle;
            settings.Version = EndPointConfig.Version1Str;
            settings.AddAuthController();
            settings.OperationProcessors.Add(new AddOdataQuery());
        }, maxEndpointVersion: EndPointConfig.Version1, addJWTBearerAuth: false);

        if (moreConfig != null)
        {
            moreConfig(builder);
        }
    }

    public static void DefaultApplication(this WebApplication app, Action<WebApplication>? moreConfig = null, Action<WebApplication>? authController = null)
    {
        app.UseDefaultExceptionHandler();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        if (authController != null)
        {
            authController(app);
        }

        app.UseFastEndpoints(config =>
        {
            config.RoutingOptions = o => o.Prefix = EndPointConfig.APIPrefix;
            config.VersioningOptions = o =>
            {
                o.Prefix = EndPointConfig.VersionPrefix;
                o.SuffixedVersion = false;
            };
        });

        app.UseOpenApi();

        app.UseSwaggerUi3(c =>
        {
            c.ConfigureDefaults();
        });

        if (moreConfig != null)
        {
            moreConfig(app);
        }
    }
}
