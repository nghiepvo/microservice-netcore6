global using FastEndpoints;
global using FastEndpoints.Security;
using Basket.API.Applications;
using Basket.API.EndPoints;
using Basket.API.Infrastructures.Redis;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration["TokenKey"]);
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version2Str;
    settings.Title = EndPointConfig.APITitle;
    settings.Version = EndPointConfig.Version2Str;
}, maxEndpointVersion: EndPointConfig.Version2)
.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version1Str;
    settings.Title = EndPointConfig.APITitle;
    settings.Version = EndPointConfig.Version1Str;
}, maxEndpointVersion: EndPointConfig.Version1);

builder.Services.AddApplication();

var app = builder.Build();

app.UseDefaultExceptionHandler();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
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

app.UseRedis();

app.Run();