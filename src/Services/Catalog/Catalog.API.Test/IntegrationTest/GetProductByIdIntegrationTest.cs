using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.API.Commons;
using Catalog.API.Domain;
using Catalog.API.EndPoints.Products;
using FastEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Catalog.API.Test.IntegrationTest.Setup;

namespace Catalog.API.Test.IntegrationTest;

[TestClass]
public class GetProductByIdIntegrationTest
{
    [TestMethod]
    public async Task Get1ProductById()
    {
        var (resp, res) = await Client.GETAsync<GetProducts, ListResponse<Product>>();

        Assert.AreEqual(HttpStatusCode.OK, resp?.StatusCode);
    }
}