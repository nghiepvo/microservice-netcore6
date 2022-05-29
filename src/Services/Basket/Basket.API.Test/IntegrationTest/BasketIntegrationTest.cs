using System.Net;
using Basket.API.Domian;
using Basket.API.EndPoints.Baskets;
using Common.Libraries.ViewModels;
using Common.LibrariesTest.Setups;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Basket.API.Test.IntegrationTest;

[TestClass]
public class BasketIntegrationTest
{
    private readonly SetupAPITest<Program> _test = new SetupAPITest<Program>(new WebApplicationFactory<Program>());

    [TestMethod]
    public async Task GetBasketSuccess_WithEmpty()
    {
        var (resp, res) = await _test.Client.GETAsync<GetBasket, TypeRequest<string>, ShoppingCart>(new() { Payload = "test_user"});

        Assert.AreEqual(HttpStatusCode.OK, resp?.StatusCode);

        Assert.AreEqual(0, res?.Items.Count);
    }
}
