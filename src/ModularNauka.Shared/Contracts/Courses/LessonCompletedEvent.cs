using ModularNauka.Shared.Events;

namespace ModularNauka.Shared.Contracts.Courses;

// Emitowany przez moduł Courses gdy lekcja zostaje ukończona.
// Obsługiwany przez moduł Users → aktualizuje postęp użytkownika.
public sealed record LessonCompletedEvent(
    Guid UserId,
    Guid LessonId,
    Guid CourseId
) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
