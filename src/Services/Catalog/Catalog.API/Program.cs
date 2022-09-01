global using FastEndpoints;
using Common.Libraries.API.Infrastructures.MongoDB;
using Common.Libraries.API.Applications;
using Common.Libraries.API.EndPoints;
using Microsoft.AspNetCore.OData;
using Common.Libraries.Configs;

var builder = WebApplication.CreateBuilder();

builder.DefaultBuilder("Catalog API", moreconfig => {
    moreconfig.Services.AddApplication();
    moreconfig.Services.AddControllers().AddOData(opt => opt.EnableQueryFeatures(5).AddRouteComponents("odata", ModelBuilder.GetEdmModel()));
});

var app = builder.Build();

app.DefaultApplication(async moreConfig => {
    await app.UseMongoDB();
}, authController => {
    authController.MapControllers().RequireAuthorization();
});

app.Run();