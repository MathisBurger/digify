using digify.AccessVoter;
using digify.Models;

namespace digify.Modules;

public interface IAuthorization
{
    string GetAuthToken(AuthClaims claims);

    AuthClaims ValidateAuth(string token);

    bool IsGranted(User user, string action, IVoter voter);
}