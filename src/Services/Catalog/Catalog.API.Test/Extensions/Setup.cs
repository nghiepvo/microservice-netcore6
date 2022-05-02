using System.Net.Http;
using System.Net.Http.Headers;
using Catalog.API.EndPoints;
using Catalog.API.EndPoints.Authentication;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Catalog.API.Test.Extensions;

public static class Setup
{
    private static readonly WebApplicationFactory<Program> factory = new();

    private const string AppSettingFile = "appsettings.Test.json";

    public static HttpClient Client { get; } = factory
        .WithWebHostBuilder(b => b.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile(AppSettingFile);
        }))
        .CreateClient();

    static Setup()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingFile);

        var configuration = builder.Build();

        if (configuration == null)
        {
            throw new ArgumentException($"The {nameof(IConfiguration)} doesn't loaded.");
        }

        var (_, result) = Client.POSTAsync<AuthenticationEndPoint, AuthenticationRequest, AuthenticationResponse>(new()
        {
            Username = configuration[EndPointConfig.Username],
            Password = configuration[EndPointConfig.Password]
        })
        .GetAwaiter()
        .GetResult();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result?.JWTToken);
    }
}