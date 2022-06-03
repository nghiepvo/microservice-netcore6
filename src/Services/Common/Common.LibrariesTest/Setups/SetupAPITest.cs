using System.Net.Http.Headers;
using Common.Libraries.Authentication;
using Common.Libraries.EndPoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Common.LibrariesTest.Setups;
public class SetupAPITest<T> where T : class
{
    private const string AppSettingFile = "appsettings.Test.json";
    public AuthenticationResponse AuthInfo { get; private set; }
    public HttpClient Client { get; private set; }
    private readonly WebApplicationFactory<T> _factory;
    public SetupAPITest(WebApplicationFactory<T> factory)
    {
        _factory = factory;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppSettingFile);

        var configuration = builder.Build();

        if (configuration == null)
        {
            throw new ArgumentException($"The {nameof(IConfiguration)} doesn't loaded.");
        }

        AuthInfo = JwtBearerGenerate.CreateTokenWithFullPermisions(configuration[EndPointConfig.TokenKey], configuration[EndPointConfig.Username], DateTime.Now.AddDays(1));

        Client = _factory.WithWebHostBuilder(b => b.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile(AppSettingFile);
            config.AddEnvironmentVariables();
        }))
        .CreateClient();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthInfo.JWTToken);
    }
}