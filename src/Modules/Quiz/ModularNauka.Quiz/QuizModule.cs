using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModularNauka.Quiz.Application;
using ModularNauka.Quiz.Infrastructure;

namespace ModularNauka.Quiz;

public static class QuizModule
{
    public static IServiceCollection AddQuizModule(this IServiceCollection services)
    {
        services.AddDbContext<QuizDbContext>(options =>
            options.UseSqlite("Data Source=quiz.db"));

        services.AddScoped<QuizService>();

        return services;
    }
}