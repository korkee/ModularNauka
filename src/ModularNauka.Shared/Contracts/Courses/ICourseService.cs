namespace ModularNauka.Shared.Contracts.Courses;

// Public API of the Courses module — used by Quiz to verify lesson existence.
public interface ICourseService
{
    Task<LessonDto?> GetLessonByIdAsync(Guid lessonId, CancellationToken ct = default);
    Task MarkLessonCompletedAsync(Guid lessonId, Guid userId, CancellationToken ct = default);
}

public record LessonDto(Guid Id, Guid CourseId, string Title, int Order);
