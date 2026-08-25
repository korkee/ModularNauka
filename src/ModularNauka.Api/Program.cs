using ModularNauka.Courses;
using ModularNauka.Quiz;
using ModularNauka.Shared.Events;
using ModularNauka.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Event Bus — jeden singleton dla ca³ej aplikacji
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();

// Rejestracja modu³ów
builder.Services.AddUsersModule();
builder.Services.AddCoursesModule();
builder.Services.AddQuizModule();

var app = builder.Build();

// Tworzenie tabel w bazach danych przy starcie
using (var scope = app.Services.CreateScope())
{
    var sp = scope.ServiceProvider;

    sp.GetRequiredService<ModularNauka.Users.Infrastructure.UsersDbContext>().Database.EnsureCreated();
    sp.GetRequiredService<ModularNauka.Courses.Infrastructure.CoursesDbContext>().Database.EnsureCreated();
    sp.GetRequiredService<ModularNauka.Quiz.Infrastructure.QuizDbContext>().Database.EnsureCreated();

    // Rejestracja handlerów eventów
    var eventBus = sp.GetRequiredService<IEventBus>();
    eventBus.RegisterUsersHandlers(sp);
    eventBus.RegisterCoursesHandlers(sp);
}

app.MapControllers();
app.Run();