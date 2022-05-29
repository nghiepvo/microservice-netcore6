global using FastEndpoints;
global using FastEndpoints.Security;
using Basket.API.Applications;
using Basket.API.Infrastructures.Redis;
using Common.Libraries.EndPoints;
using FastEndpoints.Swagger;

const string APITitle = "Basket API";

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration[EndPointConfig.TokenKey]);
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version2Str;
    settings.Title = APITitle;
    settings.Version = EndPointConfig.Version2Str;
}, maxEndpointVersion: EndPointConfig.Version2)
.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version1Str;
    settings.Title = APITitle;
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