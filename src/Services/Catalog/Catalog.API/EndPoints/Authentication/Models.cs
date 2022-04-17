using FluentValidation;

namespace Catalog.API.EndPoints.Authentication;

/// <summary>
/// Authentication API
/// </summary>
public class AuthenticationRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; }= null!;
    public string SecretKey { get; set; }= null!;
}

public class AuthenticationValidator : Validator<AuthenticationRequest>
{
    public AuthenticationValidator(IConfiguration config)
    {
        if (config is null)
            throw new ArgumentNullException(nameof(config));

        When(x => string.IsNullOrEmpty(x.SecretKey), () =>
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage(string.Format(config[MessagesConfig.Invalid], nameof(AuthenticationRequest.Username)))
            .MinimumLength(3).WithMessage(string.Format(config[MessagesConfig.Invalid], nameof(AuthenticationRequest.Username)));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(string.Format(config[MessagesConfig.Invalid], nameof(AuthenticationRequest.Password)))
                .MinimumLength(3).WithMessage(string.Format(config[MessagesConfig.Invalid], nameof(AuthenticationRequest.Password)));
        }).Otherwise(() => {
            RuleFor(x => x.SecretKey)
            .NotEqual(config[EndPointConfig.TokenKey])
            .WithMessage(string.Format(config[MessagesConfig.Invalid], nameof(AuthenticationRequest.SecretKey)));
        });

        var logger = Resolve<ILogger<AuthenticationValidator>>();
        logger?.LogInformation($"execute to validated {nameof(AuthenticationRequest)}");
    }
}

public class AuthenticationResponse
{
    public string JWTToken { get; set; } = null!;
    public DateTime ExpiryDate { get; set; }
    public IEnumerable<string> Permissions { get; set; } = null!;
}