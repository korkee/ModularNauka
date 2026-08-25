using ModularNauka.Shared.Contracts.Courses;
using ModularNauka.Shared.Events;

namespace ModularNauka.Users.Application;

public class LessonCompletedHandler : IEventHandler<LessonCompletedEvent>
{
    private readonly UserService _userService;

    public LessonCompletedHandler(UserService userService)
    {
        _userService = userService;
    }

    public async Task HandleAsync(LessonCompletedEvent @event, CancellationToken ct = default)
    {
        await _userService.UpdateProgressAsync(@event.UserId, @event.CourseId, 0, ct);
    }
}
