using digify.Fixtures;
using digify.Models;
using digify.Modules;
using Microsoft.EntityFrameworkCore;

namespace digify.Shared;

public class DatabaseContext : DbContext, IContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Class student relation
        modelBuilder.Entity<Class>()
            .HasMany(c => c.Students)
            .WithOne(s => s.schoolClass);

        // Teacher class relation
        modelBuilder.Entity<Class>()
            .HasMany(c => c.Teachers)
            .WithMany(t => t.classes);
    }
    
}