using System.Net;
using Common.Libraries.API.Applications;
using Common.Libraries.API.Domain;
using Common.Libraries.API.EndPoints;
using Common.Libraries.API.EndPoints.Controllers;
using Common.Libraries.API.Infrastructures.MongoDB;
using Common.Libraries.API.Infrastructures.MongoDB.Migrations;
using Common.Libraries.Test.Odata;
using Common.Libraries.Test.Setups;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Common.Libraries.API.Test.IntegrationTest;

public class ConfigStartup : TestStartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureControllers(typeof(ProductsController));
        services.AddApplication();
        services.AddMongoDB();
        services.AddControllers().AddOData(options => options.AddRouteComponents("odata", ModelBuilder.GetEdmModel()).EnableQueryFeatures(5));
    }
}


public class ProductsOdataIntegrationTest: SetupOdataTest<ConfigStartup>
{
    public ProductsOdataIntegrationTest(WebODataTestFixture<ConfigStartup> fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetProductsOdataWithCountSuccess()
    {
        // Arrange
        var queryUrl = "odata/Products?$count=true";

        // Act
        var (res, resp, count, nextLink) = await Client.SENDODataAsync<List<Product>> (queryUrl);

        // Assert
        Assert.Equal(HttpStatusCode.OK, res?.StatusCode);

        Assert.True(count > 0);

        Assert.NotEmpty(nextLink);

        Assert.Equal(ProductMigrationData.Products.First().ID, resp?.FirstOrDefault()?.ID);
    }
}