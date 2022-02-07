using digify.Fixtures;
using digify.Models;
using digify.Modules;
using Microsoft.EntityFrameworkCore;

namespace digify.Shared;

public class DatabaseContext : DbContext, IContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<TeacherClass> TeacherClasses { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Class student relation
        modelBuilder.Entity<Class>()
            .HasMany(c => c.Students)
            .WithOne(s => s.SchoolClass)
            .HasForeignKey(c => c.SchoolClassId);

        // Teacher class relation
        modelBuilder.Entity<TeacherClass>().HasKey(tc => new {tc.TeacherId, tc.ClassId});
        modelBuilder.Entity<TeacherClass>()
            .HasOne<Class>(tc => tc.Class)
            .WithMany(c => c.Teachers)
            .HasForeignKey(tc => tc.ClassId);
        modelBuilder.Entity<TeacherClass>()
            .HasOne<User>(tc => tc.Teacher)
            .WithMany(t => t.Classes)
            .HasForeignKey(tc => tc.TeacherId);
    }
    
}