namespace ModularNauka.Shared.Contracts.Courses;

// Publiczne API modułu Courses — Quiz używa tego do weryfikacji czy lekcja istnieje.
public interface ICourseService
{
    Task<LessonDto?> GetLessonByIdAsync(Guid lessonId, CancellationToken ct = default);
    Task MarkLessonCompletedAsync(Guid lessonId, Guid userId, CancellationToken ct = default);
}

public record LessonDto(Guid Id, Guid CourseId, string Title, int Order);
