using ModularNauka.Shared.Events;

namespace ModularNauka.Shared.Contracts.Quiz;

// Emitted by the Quiz module when a user completes a quiz.
// Handled by the Courses module → marks the lesson as completed.
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
