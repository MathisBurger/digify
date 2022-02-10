using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Fixtures;

/// <summary>
/// UserFixture that is used for creating
/// initial users.
/// </summary>
public class UserFixture : IFixture
{
    private readonly IContext db;
    private readonly IPasswordHasher hasher;

    public const string ADMIN_USERNAME = "admin";
    public const string ADMIN_PASSWORD = "123";

    public UserFixture(IContext _db, IPasswordHasher _hasher)
    {
        db = _db;
        hasher = _hasher;
    }

    /// <inheritdoc cref="IFixture" />
    public async Task Load()
    {
        var adminUser = new User();
        adminUser.Username = ADMIN_USERNAME;
        adminUser.Password = hasher.HashFromPassword(ADMIN_PASSWORD);
        adminUser.Roles = new string[] {UserRoles.ADMIN};
        db.Add(adminUser);
        await db.SaveChangesAsync();
    }

    /// <inheritdoc cref="IFixture" />
    public async Task<bool> NotExists() =>
        (await db.Users.Where(u => u.Username == ADMIN_USERNAME).FirstOrDefaultAsync()) == null;

}