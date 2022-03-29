using digify.Fixtures;
using digify.Models;
using digify.Modules;
using Microsoft.EntityFrameworkCore;

namespace digify.Shared;

/// <summary>
/// Core database context used for communication with the postgreSQL server.
/// </summary>
public class DatabaseContext : DbContext, IContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Timetable> Timetables { get; set; }
    public DbSet<TimeTableElement> TimeTableElements { get; set; }
    public DbSet<Classbook> Classbooks { get; set; }
    public DbSet<ClassbookDayEntry> ClassbookDayEntries { get; set; }
    public DbSet<ClassbookDayEntryLesson> ClassbookDayEntryLessons { get; set; }

    public DatabaseContext(DbContextOptions options) : base(options)
    { }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>()
            .HasOne(u => u.SchoolClass)
            .WithMany(c => c.Students)
            .OnDelete(DeleteBehavior.SetNull);
    }
}