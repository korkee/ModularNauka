using ModularNauka.Shared.Events;

namespace ModularNauka.Shared.Contracts.Courses;

// Emitted by the Courses module when a lesson is completed.
// Handled by the Users module → updates user progress.
public sealed record LessonCompletedEvent(
    Guid UserId,
    Guid LessonId,
    Guid CourseId
) : IEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
