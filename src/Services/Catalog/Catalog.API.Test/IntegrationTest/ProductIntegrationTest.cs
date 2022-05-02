using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.API.Commons;
using Catalog.API.Domain;
using Catalog.API.EndPoints.Products;
using Catalog.API.Infrastructures.MongoDB.MasterData;
using FastEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Catalog.API.Test.Extensions.Setup;

namespace Catalog.API.Test.IntegrationTest;

[TestClass]
public class ProductIntegrationTest
{
    [TestMethod]
    public async Task GetProductsSuccess()
    {
        var (resp, res) = await Client.GETAsync<GetProducts, ListResponse<Product>>();

        Assert.AreEqual(HttpStatusCode.OK, resp?.StatusCode);
        Assert.AreEqual(ProductData.Products.Length, res?.Data.Count());
    }

    [TestMethod]
    public async Task CRUDProductSuccess()
    {
        var product = ProductData.Products.First();

        product.ID = Guid.NewGuid().ToString("N");

        // Add Product

        var respPOST = await Client.POSTAsync<AddProduct, Product>(product);

        Assert.AreEqual(HttpStatusCode.OK, respPOST?.StatusCode);


        // Read Product

        var (respGET, resGET) = await Client.GETAsync<GetProductById, IdRequest<string>, Product>(new() { Id = product.ID });

        Assert.AreEqual(HttpStatusCode.OK, respGET?.StatusCode);
        Assert.AreEqual(product.ID, resGET?.ID);

        // Update Product
        product.Description = new string(product.Description.Reverse().ToArray());

        var (respPUT, resPUT) = await Client.PUTAsync<UpdateProductById, Product, TypeResponse<bool>>(product);

        Assert.AreEqual(HttpStatusCode.OK, respPUT?.StatusCode);
        Assert.IsTrue(resPUT?.Body);

        //Delete Product
        var (respDELETE, resDLETE) = await Client.DELETEAsync<DeleteProductById, IdRequest<string>, TypeResponse<bool>>(new() { Id = product.ID});

        Assert.AreEqual(HttpStatusCode.OK, respDELETE?.StatusCode);
        Assert.IsTrue(resDLETE?.Body);
    }
}