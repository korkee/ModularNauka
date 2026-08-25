using ModularNauka.Shared.Contracts.Quiz;
using ModularNauka.Shared.Events;

namespace ModularNauka.Courses.Application;

public class QuizSubmittedHandler : IEventHandler<QuizSubmittedEvent>
{
    private readonly CourseService _courseService;

    public QuizSubmittedHandler(CourseService courseService)
    {
        _courseService = courseService;
    }

    public async Task HandleAsync(QuizSubmittedEvent @event, CancellationToken ct = default)
    {
        await _courseService.MarkLessonCompletedAsync(@event.LessonId, @event.UserId, ct);
    }
}