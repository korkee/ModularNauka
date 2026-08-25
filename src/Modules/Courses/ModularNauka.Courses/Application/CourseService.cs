using Microsoft.EntityFrameworkCore;
using ModularNauka.Courses.Domain;
using ModularNauka.Courses.Infrastructure;
using ModularNauka.Shared.Contracts.Courses;
using ModularNauka.Shared.Events;

namespace ModularNauka.Courses.Application;

public class CourseService : ICourseService
{
    private readonly CoursesDbContext _db;
    private readonly IEventBus _eventBus;

    public CourseService(CoursesDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }

    public async Task<LessonDto?> GetLessonByIdAsync(Guid lessonId, CancellationToken ct = default)
    {
        var lesson = await _db.Lessons.FindAsync([lessonId], ct);
        if (lesson is null) return null;
        return new LessonDto(lesson.Id, lesson.CourseId, lesson.Title, lesson.Order);
    }

    public async Task MarkLessonCompletedAsync(Guid lessonId, Guid userId, CancellationToken ct = default)
    {
        var lesson = await _db.Lessons.FindAsync([lessonId], ct);
        if (lesson is null) return;

        lesson.MarkCompleted();
        await _db.SaveChangesAsync(ct);

        await _eventBus.PublishAsync(new LessonCompletedEvent(userId, lessonId, lesson.CourseId), ct);
    }

    public async Task<Course> CreateCourseAsync(string title, string description, CancellationToken ct = default)
    {
        var course = Course.Create(title, description);
        _db.Courses.Add(course);
        await _db.SaveChangesAsync(ct);
        return course;
    }

    public async Task<Lesson> AddLessonAsync(Guid courseId, string title, int order, CancellationToken ct = default)
    {
        var lesson = Lesson.Create(courseId, title, order);
        _db.Lessons.Add(lesson);
        await _db.SaveChangesAsync(ct);
        return lesson;
    }

    public async Task<List<Course>> GetAllCoursesAsync(CancellationToken ct = default)
    {
        return await _db.Courses.ToListAsync(ct);
    }

    public async Task<List<Lesson>> GetLessonsByCourseAsync(Guid courseId, CancellationToken ct = default)
    {
        return await _db.Lessons
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.Order)
            .ToListAsync(ct);
    }
}