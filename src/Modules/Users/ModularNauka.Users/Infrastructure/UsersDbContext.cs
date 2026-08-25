using Microsoft.EntityFrameworkCore;
using ModularNauka.Users.Domain;

namespace ModularNauka.Users.Infrastructure;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProgress> UserProgresses => Set<UserProgress>();

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Name).IsRequired().HasMaxLength(100);
            e.Property(u => u.Email).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<UserProgress>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasIndex(p => new { p.UserId, p.CourseId }).IsUnique();
        });
    }
}