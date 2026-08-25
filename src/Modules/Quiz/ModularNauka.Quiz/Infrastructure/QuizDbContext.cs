using Microsoft.EntityFrameworkCore;
using ModularNauka.Quiz.Domain;
using QuizEntity = ModularNauka.Quiz.Domain.Quiz;

namespace ModularNauka.Quiz.Infrastructure;

public class QuizDbContext : DbContext
{
    public DbSet<QuizEntity> Quizzes => Set<QuizEntity>();
    public DbSet<Question> Questions => Set<Question>();

    public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuizEntity>(e =>
        {
            e.HasKey(q => q.Id);
            e.Property(q => q.Title).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Question>(e =>
        {
            e.HasKey(q => q.Id);
            e.Property(q => q.Text).IsRequired().HasMaxLength(500);
            e.Property(q => q.CorrectAnswer).IsRequired().HasMaxLength(200);
            e.Property(q => q.Options).HasConversion(
                list => string.Join("||", list),
                str => str.Split("||", StringSplitOptions.None).ToList()
            );
        });
    }
}