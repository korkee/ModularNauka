namespace ModularNauka.Shared.Contracts.Users;

// Public API of the Users module — other modules depend on this interface
// instead of referencing the implementation directly.
public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid userId, CancellationToken ct = default);
    Task UpdateProgressAsync(Guid userId, Guid courseId, int completedLessons, CancellationToken ct = default);
}

public record UserDto(Guid Id, string Name, string Email);
