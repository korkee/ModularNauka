using Microsoft.AspNetCore.Mvc;
using ModularNauka.Courses.Application;

namespace ModularNauka.Courses.Api;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
	private readonly CourseService _courseService;

	public CoursesController(CourseService courseService)
	{
		_courseService = courseService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll(CancellationToken ct)
	{
		var courses = await _courseService.GetAllCoursesAsync(ct);
		return Ok(courses.Select(c => new { c.Id, c.Title, c.Description }));
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateCourseRequest request, CancellationToken ct)
	{
		var course = await _courseService.CreateCourseAsync(request.Title, request.Description, ct);
		return Ok(new { course.Id, course.Title });
	}

	[HttpGet("{courseId:guid}/lessons")]
	public async Task<IActionResult> GetLessons(Guid courseId, CancellationToken ct)
	{
		var lessons = await _courseService.GetLessonsByCourseAsync(courseId, ct);
		return Ok(lessons.Select(l => new { l.Id, l.Title, l.Order, l.IsCompleted }));
	}

	[HttpPost("{courseId:guid}/lessons")]
	public async Task<IActionResult> AddLesson(Guid courseId, [FromBody] AddLessonRequest request, CancellationToken ct)
	{
		var lesson = await _courseService.AddLessonAsync(courseId, request.Title, request.Order, ct);
		return Ok(new { lesson.Id, lesson.Title, lesson.Order });
	}
}

public record CreateCourseRequest(string Title, string Description);
public record AddLessonRequest(string Title, int Order);