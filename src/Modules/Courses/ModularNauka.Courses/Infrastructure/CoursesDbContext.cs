using Microsoft.EntityFrameworkCore;
using ModularNauka.Courses.Domain;

namespace ModularNauka.Courses.Infrastructure;

public class CoursesDbContext : DbContext
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Lesson> Lessons => Set<Lesson>();

    public CoursesDbContext(DbContextOptions<CoursesDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Title).IsRequired().HasMaxLength(200);
            e.Property(c => c.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<Lesson>(e =>
        {
            e.HasKey(l => l.Id);
            e.Property(l => l.Title).IsRequired().HasMaxLength(200);
            e.HasIndex(l => new { l.CourseId, l.Order }).IsUnique();
        });
    }
}