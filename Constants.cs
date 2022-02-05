namespace digify;

/// <summary>
/// All constants in the system
/// </summary>
public class Constants
{
    /// <summary>
    /// The duration of a web session
    /// </summary>
    public static readonly TimeSpan SESSION_EXPIRATION = TimeSpan.FromDays(7);

    /// <summary>
    /// The name of the session cookie
    /// </summary>
    public const string SESSION_COOKIE_NAME = "digify.session";
}