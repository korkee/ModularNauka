namespace ModularNauka.Shared.Contracts.Users;

// Publiczne API modułu Users — inne moduły używają tego interfejsu zamiast
// bezpośredniego odwołania do implementacji.
public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid userId, CancellationToken ct = default);
    Task UpdateProgressAsync(Guid userId, Guid courseId, int completedLessons, CancellationToken ct = default);
}

public record UserDto(Guid Id, string Name, string Email);
