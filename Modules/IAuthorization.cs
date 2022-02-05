using digify.AccessVoter;
using digify.Models;

namespace digify.Modules;

/// <summary>
/// Scaffold for the authorization service
/// </summary>
public interface IAuthorization
{
    /// <summary>
    /// Creates an auth token from the claims.
    /// </summary>
    /// <param name="claims">The auth claims of the user</param>
    /// <returns>The token that is used for authentification</returns>
    string GetAuthToken(AuthClaims claims);

    /// <summary>
    /// Checks if the token is valid
    /// </summary>
    /// <param name="token">The provided token</param>
    /// <returns>The auth claims from the token</returns>
    AuthClaims ValidateAuth(string token);

    /// <summary>
    /// Checks if a user is allowed to perform
    /// a action.
    /// </summary>
    /// <param name="user">The user that performs the action</param>
    /// <param name="action">The key of the action that should be performed</param>
    /// <param name="voter">The instance of the voter that validates this method</param>
    /// <returns>If the user is allowed to perform the action</returns>
    bool IsGranted(User user, string action, IVoter voter);
}