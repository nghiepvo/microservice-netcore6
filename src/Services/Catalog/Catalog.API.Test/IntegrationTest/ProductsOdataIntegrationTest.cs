using System.Net;
using Catalog.API.Domain;
using Catalog.API.Infrastructures.MongoDB.Migrations;
using Catalog.API.Test.Extensions;
using Catalog.API.Test.Extensions.Odata;
using Xunit;

namespace Catalog.API.Test.IntegrationTest;
public class ProductsOdataIntegrationTest: SetupXUnit
{
    public ProductsOdataIntegrationTest(WebODataTestFixture<Startup> fixture) : base(fixture)
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