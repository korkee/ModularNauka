using ModularNauka.Shared.Events;

namespace ModularNauka.Shared.Contracts.Quiz;

// Emitowany przez moduł Quiz gdy użytkownik ukończy quiz.
// Obsługiwany przez moduł Courses → oznacza lekcję jako ukończoną.
public sealed record QuizSubmittedEvent(
    Guid UserId,
    Guid LessonId,
    Guid CourseId,
    int Score,
    int MaxScore
) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
