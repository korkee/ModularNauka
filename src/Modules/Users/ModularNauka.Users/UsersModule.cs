using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModularNauka.Shared.Contracts.Courses;
using ModularNauka.Shared.Contracts.Users;
using ModularNauka.Shared.Events;
using ModularNauka.Users.Application;
using ModularNauka.Users.Infrastructure;

namespace ModularNauka.Users;

public static class UsersModule
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>(options =>
            options.UseSqlite("Data Source=users.db"));

        services.AddScoped<UserService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<LessonCompletedHandler>();

        return services;
    }

    public static void RegisterUsersHandlers(this IEventBus eventBus, IServiceProvider sp)
    {
        eventBus.Subscribe<LessonCompletedEvent>(sp.GetRequiredService<LessonCompletedHandler>());
    }
}