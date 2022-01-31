using digify.Models;
using digify.Modules;
using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Fixtures;

public class UserFixture : IFixture
{
    private readonly DatabaseContext db;
    private readonly IPasswordHasher hasher;

    public const string ADMIN_USERNAME = "admin";
    public const string ADMIN_PASSWORD = "123";

    public UserFixture(DatabaseContext _db, IPasswordHasher _hasher)
    {
        db = _db;
        hasher = _hasher;
    }

    public async Task Load()
    {
        var adminUser = new User();
        adminUser.Username = ADMIN_USERNAME;
        adminUser.Password = hasher.HashFromPassword(ADMIN_PASSWORD);
        adminUser.Roles = new string[] {User.ROLE_ADMIN};
        db.Users.Add(adminUser);
        await db.SaveChangesAsync();
    }

    public async Task<bool> NotExists() =>
        (await db.Users.Where(u => u.Username == ADMIN_USERNAME).FirstOrDefaultAsync()) == null;

}