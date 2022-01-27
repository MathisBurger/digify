using digify.Shared;
using Microsoft.EntityFrameworkCore;

namespace digify.Models;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options): base(options) {}
    
    public DbSet<User> Users { get; set; }

}