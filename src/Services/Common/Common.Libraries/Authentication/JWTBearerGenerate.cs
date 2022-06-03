using Commons.Libraries.Commons;
using FastEndpoints.Security;

namespace Common.Libraries.Authentication;

public static class JwtBearerGenerate
{
    public static AuthenticationResponse CreateToken(string signingKey, DateTime expireAt, (string claimType, string claimValue)[] claims, string[] permissions)
    {
        var jwtToken = JWTBearer.CreateToken(
            signingKey: signingKey,
            expireAt: expireAt,
            claims: claims,
            permissions: permissions);

        return new AuthenticationResponse
        {
            ExpiryDate = expireAt,
            JWTToken = jwtToken,
            Permissions = permissions
        };
    }

    public static AuthenticationResponse CreateTokenWithFullPermisions(string signingKey, string username, DateTime expireAt)
    {
        var permissions = new[]
        {
            Allow.ProductCreate,
            Allow.ProductRead,
            Allow.ProductUpdate,
            Allow.ProductDelete,
            Allow.BasketRead,
            Allow.BasketUpdate,
            Allow.BasketDelete
        };

        return CreateToken(signingKey, expireAt, new [] { (nameof(AuthenticationRequest.Username), username) }, permissions);
    }
}
