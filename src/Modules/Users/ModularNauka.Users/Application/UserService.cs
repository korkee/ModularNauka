using Microsoft.EntityFrameworkCore;
using ModularNauka.Shared.Contracts.Users;
using ModularNauka.Users.Domain;
using ModularNauka.Users.Infrastructure;

namespace ModularNauka.Users.Application;

public class UserService : IUserService
{
    private readonly UsersDbContext _db;

    public UserService(UsersDbContext db)
    {
        _db = db;
    }

    public async Task<UserDto?> GetByIdAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _db.Users.FindAsync([userId], ct);
        if (user is null) return null;

        return new UserDto(user.Id, user.Name, user.Email);
    }

    public async Task UpdateProgressAsync(Guid userId, Guid courseId, int completedLessons, CancellationToken ct = default)
    {
        var progress = await _db.UserProgresses
            .FirstOrDefaultAsync(p => p.UserId == userId && p.CourseId == courseId, ct);

        if (progress is null)
        {
            progress = UserProgress.Start(userId, courseId);
            _db.UserProgresses.Add(progress);
        }
        else
        {
            progress.IncrementLesson();
        }

        await _db.SaveChangesAsync(ct);
    }

    public async Task<User> RegisterAsync(string name, string email, CancellationToken ct = default)
    {
        var user = User.Create(name, email);
        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);
        return user;
    }
}