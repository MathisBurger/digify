using digify.Fixtures;
using digify.Modules;

namespace digify.Shared;

public class FixtureLoader
{
    private readonly ILogger<FixtureLoader> Logger;
    private readonly DatabaseContext Context;
    private readonly IPasswordHasher Hasher;

    public FixtureLoader(ILogger<FixtureLoader> _logger, DatabaseContext _context, IPasswordHasher _hasher)
    {
        Logger = _logger;
        Context = _context;
        Hasher = _hasher;
    }
    
    public async Task Load()
    {
        var userFixture = new UserFixture(Context, Hasher);
        if (await userFixture.NotExists()) userFixture.Load();
        
        Logger.LogInformation("Loaded fixtures");
    }
}