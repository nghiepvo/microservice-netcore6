using System.Net;
using System.Net.Http.Headers;
using Catalog.API.Domain;
using Catalog.API.Infrastructures.MongoDB.MasterData;
using Catalog.API.Test.Extensions;
using Catalog.API.Test.Extensions.Odata;
using Newtonsoft.Json.Linq;
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
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, queryUrl);
        request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json;odata.metadata=none"));

        // Act
        HttpResponseMessage response = await Client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var jTokenParse = JToken.Parse(await response.Content.ReadAsStringAsync());

        var products = jTokenParse["value"].ToObject<List<Product>>();

        var count = jTokenParse["@odata.count"].ToObject<int>();

        Assert.True(count > 0);

        Assert.Equal(ProductData.Products.First().ID, products.First().ID);
    }

}