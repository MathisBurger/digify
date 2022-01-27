using digify.Models;
using Microsoft.EntityFrameworkCore;

namespace digify.Shared;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }

    private readonly IConfiguration Configuration;
    public DatabaseContext(IConfiguration configuration)
    {
        this.Configuration = configuration;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = this.Configuration.GetValue<string>("database:postgresConnectionString");
        optionsBuilder.UseNpgsql(connectionString);
        
        base.OnConfiguring(optionsBuilder);
    }
}