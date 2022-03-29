using digify.Fixtures;
using digify.Modules;

namespace digify.Shared;

/// <summary>
/// Loads all fixtures in the system
/// </summary>
public class FixtureLoader : IFixtureLoader
{
    private readonly ILogger<FixtureLoader> Logger;
    private readonly IPasswordHasher Hasher;

    public FixtureLoader(ILogger<FixtureLoader> _logger, IPasswordHasher _hasher)
    {
        Logger = _logger;
        Hasher = _hasher;
    }
    
    /// <summary>
    /// Loads all fixtures
    /// </summary>
    /// <param name="context">The database context</param>
    public async Task Load(IContext context)
    {
        var userFixture = new UserFixture(context, Hasher);
        if (await userFixture.NotExists()) await userFixture.Load();

        Logger.LogInformation("Loaded fixtures");
    }
}