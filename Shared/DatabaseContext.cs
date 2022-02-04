using digify.Fixtures;
using digify.Models;
using digify.Modules;
using Microsoft.EntityFrameworkCore;

namespace digify.Shared;

public class DatabaseContext : DbContext, IContext
{
    public DbSet<User> Users { get; set; }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    { }
}