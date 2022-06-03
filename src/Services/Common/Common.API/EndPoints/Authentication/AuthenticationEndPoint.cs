using Common.Libraries.Authentication;
using Common.Libraries.EndPoints;

namespace Common.API.EndPoints.Authentication;

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
            var expireAt = DateTime.UtcNow.AddDays(1);

            await SendAsync
            (
                JwtBearerGenerate.CreateTokenWithFullPermisions(_configuration[EndPointConfig.TokenKey], req.Username, expireAt),
                cancellation: ct
            );
        }
        else
        {
            AddError(string.Format(_configuration[MessagesConfig.Fail], Route));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}