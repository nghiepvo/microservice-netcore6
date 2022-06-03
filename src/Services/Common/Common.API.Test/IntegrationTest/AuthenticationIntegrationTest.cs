using System.Net;
using Common.API.EndPoints.Authentication;
using Common.Libraries.Authentication;
using Common.LibrariesTest.Setups;
using FastEndpoints;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.API.Test.IntegrationTest;

[TestClass]
public class AuthenticationTest
{
    private readonly SetupAPITest<Program> _test = new SetupAPITest<Program>(new WebApplicationFactory<Program>());

    [TestMethod]
    public async Task LoginSuccess()
    {
        var (resp, res) = await _test.Client.POSTAsync<AuthenticationEndPoint, AuthenticationRequest, AuthenticationResponse>(new()
        {
            Username = "nghiepvo",
            Password = "123456"
        });

        Assert.AreEqual(HttpStatusCode.OK, resp?.StatusCode);
        Assert.IsNotNull(res?.JWTToken);
        Assert.AreNotEqual(0, res?.Permissions.Count);
    }

    [TestMethod]
    public async Task LoginFair()
    {
        var (resp, res) = await _test.Client.POSTAsync<AuthenticationEndPoint, AuthenticationRequest, ErrorResponse>(new()
        {
            Username = "test",
            Password = "123456"
        });

        Assert.AreEqual(HttpStatusCode.BadRequest, resp?.StatusCode);
        Assert.AreNotEqual(2, res?.Errors);
    }
}
