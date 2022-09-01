global using FastEndpoints;
using Basket.API.Applications;
using Basket.API.Infrastructures.Redis;
using Common.Libraries.Configs;

var builder = WebApplication.CreateBuilder();

builder.DefaultBuilder("Basket API", moreConfig => {
    moreConfig.Services.AddApplication();
});

var app = builder.Build();

app.DefaultApplication(moreConfig => {
    app.UseRedis();
});

app.Run();