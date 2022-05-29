using Common.API.EndPoints;
using Common.API.EndPoints.Authentication;
using Common.Libraries.Authentication;
using FakeItEasy;
using FastEndpoints;
using FastEndpoints.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.API.Test;

[TestClass]
public class AuthenticationEndPointUnitTest
{
    [TestMethod]
    public async Task LoginSuccess()
    {
        //arrange
        var req = new AuthenticationRequest
        {
            Username = "nghiepvo",
            Password = "pass"
        };
        var fakeConfig = A.Fake<IConfiguration>();
        A.CallTo(() => fakeConfig["TokenKey"]).Returns("0000000000000000");
        A.CallTo(() => fakeConfig["BaseAuthentication:Username"]).Returns(req.Username);
        A.CallTo(() => fakeConfig["BaseAuthentication:Password"]).Returns(req.Password);

        var ep = Factory.Create<AuthenticationEndPoint>(fakeConfig);

        //act
        await ep.HandleAsync(req, default);
        var rsp = ep.Response;

        //assert
        Assert.IsFalse(ep.ValidationFailed);
        Assert.IsNotNull(rsp);
        Assert.IsTrue(rsp.Permissions.Count > 0);
    }

    [TestMethod]
    public async Task LoginFail()
    {
        //arrange
        var errorMessage = "{0} is fail.";
        var fakeConfig = A.Fake<IConfiguration>();
        A.CallTo(() => fakeConfig["TokenKey"]).Returns("0000000000000000");
        A.CallTo(() => fakeConfig[MessagesConfig.Fail]).Returns(errorMessage);
        var req = new AuthenticationRequest
        {
            Username = "nghiepvo",
            Password = "pass"
        };

        var ep = Factory.Create<AuthenticationEndPoint>(fakeConfig);

        //assert
        await Assert.ThrowsExceptionAsync<ValidationFailureException>(async () => { await ep.HandleAsync(req, default); });
    }
}
