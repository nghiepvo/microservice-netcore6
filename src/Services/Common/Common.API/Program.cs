global using FastEndpoints;
using Common.Libraries.Configs;

var builder = WebApplication.CreateBuilder();

builder.DefaultBuilder("Common API");

var app = builder.Build();

app.DefaultApplication();

app.Run();