using ModularNauka.Courses;
using ModularNauka.Quiz;
using ModularNauka.Shared.Events;
using ModularNauka.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Event Bus � jeden singleton dla ca�ej aplikacji
builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();

// Rejestracja modu��w
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
    eventBus.RegisterUsersHandlers();
    eventBus.RegisterCoursesHandlers();
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();