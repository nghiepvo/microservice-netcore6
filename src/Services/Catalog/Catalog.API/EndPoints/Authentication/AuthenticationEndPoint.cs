using Catalog.API.Commons;

namespace Catalog.API.EndPoints.Authentication;

public class AuthenticationEndPoint : Endpoint<AuthenticationRequest, AuthenticationResponse>
{
    public const string Route = "Authentication";
    private readonly IConfiguration _configuration;

    public AuthenticationEndPoint(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes(Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AuthenticationRequest req, CancellationToken ct)
    {
        if (req.Username == _configuration[EndPointConfig.Username] && req.Password == _configuration[EndPointConfig.Password])
        {
            var permissions = new[]
            {
                Allow.ProductCreate,
                Allow.ProductRead,
                Allow.ProductUpdate,
                Allow.ProductDelete
            };
            var expireDate = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(
                signingKey: _configuration[EndPointConfig.TokenKey],
                expireAt: expireDate,
                claims: new[]
                {
                    (nameof(AuthenticationRequest.Username), req.Username)
                },
                permissions: permissions);

            await SendAsync(new AuthenticationResponse
            {
                ExpiryDate = expireDate,
                JWTToken = jwtToken,
                Permissions = permissions
            });
        }
        else
        {
            ThrowError(string.Format(_configuration[MessagesConfig.Fail], Route));
        }
    }
}