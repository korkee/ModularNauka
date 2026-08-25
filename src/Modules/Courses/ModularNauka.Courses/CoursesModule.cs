using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModularNauka.Shared.Contracts.Courses;
using ModularNauka.Shared.Contracts.Quiz;
using ModularNauka.Shared.Events;
using ModularNauka.Courses.Application;
using ModularNauka.Courses.Infrastructure;

namespace ModularNauka.Courses;

public static class CoursesModule
{
    public static IServiceCollection AddCoursesModule(this IServiceCollection services)
    {
        services.AddDbContext<CoursesDbContext>(options =>
            options.UseSqlite("Data Source=courses.db"));

        services.AddScoped<CourseService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<QuizSubmittedHandler>();

        return services;
    }

    public static void RegisterCoursesHandlers(this IEventBus eventBus)
    {
        eventBus.Subscribe<QuizSubmittedEvent>(new QuizSubmittedHandler(null!));
    }
}