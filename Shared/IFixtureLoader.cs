namespace digify.Shared;

/// <summary>
/// Scaffold for fixture loader
/// </summary>
public interface IFixtureLoader
{
    /// <summary>
    /// Loads all fixtures
    /// </summary>
    /// <param name="context">The database connection</param>
    Task Load(IContext context);
}