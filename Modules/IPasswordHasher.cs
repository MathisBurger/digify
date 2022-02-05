namespace digify.Modules;

/// <summary>
/// Scaffold for the password hashing service
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the password
    /// </summary>
    /// <param name="password">The password that should he hashed</param>
    /// <returns>The hashed password</returns>
    string HashFromPassword(string password);

    /// <summary>
    /// Checks if the password and the hash are same.
    /// </summary>
    /// <param name="hash">The hashed password</param>
    /// <param name="password">The password in clear text</param>
    /// <returns>If the hashed value of the password is same as the hash</returns>
    bool CompareHashAndPassword(string hash, string password);
}