global using FastEndpoints;
global using FastEndpoints.Security;
using FastEndpoints.Swagger;

using Catalog.API.Infrastructures.MongoDB;
using Catalog.API.Applications;
using Catalog.API.EndPoints;
using Microsoft.AspNetCore.OData;
using Catalog.API.Infrastructures.Nswag;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.AddAuthenticationJWTBearer(builder.Configuration["TokenKey"]);
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version2Str;
    settings.Title = EndPointConfig.APITitle;
    settings.Version = EndPointConfig.Version2Str;
    settings.AddAuthController();
    settings.OperationProcessors.Add(new AddOdataQuery());
}, maxEndpointVersion: EndPointConfig.Version2, addJWTBearerAuth: false)
.AddSwaggerDoc(settings =>
{
    settings.DocumentName = EndPointConfig.Version1Str;
    settings.Title = EndPointConfig.APITitle;
    settings.Version = EndPointConfig.Version1Str;
    settings.AddAuthController();
    settings.OperationProcessors.Add(new AddOdataQuery());
}, maxEndpointVersion: EndPointConfig.Version1, addJWTBearerAuth: false);

builder.Services.AddApplication();
builder.Services.AddControllers().AddOData(opt => opt.EnableQueryFeatures(5).AddRouteComponents("odata", ModelBuilder.GetEdmModel()));

var app = builder.Build();

app.UseDefaultExceptionHandler();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

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

await app.UseMongoDB();

app.Run();