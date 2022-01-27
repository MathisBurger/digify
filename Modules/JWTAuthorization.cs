using System.Text;
using digify.Models;
using digify.Util;

namespace digify.Modules;

using JWT.Algorithms;
using JWT.Builder;

public class JWTAuthorization: IAuthorization
{
    private readonly ILogger<JWTAuthorization> logger;
    private readonly byte[] signingKey;
    private readonly TimeSpan expiration;

    private JwtBuilder Builder
    {
        get => new JwtBuilder().WithSecret(signingKey).WithAlgorithm(new HMACSHA256Algorithm());
    }

    public JWTAuthorization(ILogger<JWTAuthorization> _logger, byte[] _signingKey, TimeSpan _expiration)
    {
        logger = _logger;
        signingKey = _signingKey;
        expiration = _expiration;
    }

    public JWTAuthorization(ILogger<JWTAuthorization> _logger, byte[] _signingKey) :
        this(_logger, _signingKey, Constants.SESSION_EXPIRATION) {}
    
    public JWTAuthorization(ILogger<JWTAuthorization> _logger, string _signingKey) :
        this(_logger, Encoding.UTF8.GetBytes(_signingKey), Constants.SESSION_EXPIRATION) {}

    public JWTAuthorization(ILogger<JWTAuthorization> _logger) :
        this(_logger, CryptoRandom.GetBytes(256))
    {
        logger.LogInformation("JWt key has been generated");
    }

    public string GetAuthToken(AuthClaims claims) =>
        Builder.AddClaim("sub", claims.SessionId)
            .AddClaim("uid", claims.UserId)
            .ExpirationTime(DateTime.Now.Add(expiration))
            .IssuedAt(DateTime.Now)
            .Encode();

    public AuthClaims ValidateAuth(string token) =>
        new AuthClaims(Builder.MustVerifySignature().Decode<IDictionary<string, object>>(token));
}