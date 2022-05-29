using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Common.LibrariesTest.Odata;

public class WebODataTestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private const string AppSettingFile = "appsettings.Test.json";
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseStartup<TStartup>()
            .ConfigureAppConfiguration((host, config) => {
                config.AddJsonFile(AppSettingFile);
                config.AddEnvironmentVariables();
            })

            // we have to set the root otherwise we get the System.IO.DirectoryNotFoundException
            .UseContentRoot("");
    }

    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder();
    }
}