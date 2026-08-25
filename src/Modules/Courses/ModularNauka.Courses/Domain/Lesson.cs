namespace ModularNauka.Courses.Domain;

public class Lesson
{
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public int Order { get; private set; }
    public bool IsCompleted { get; private set; }

    public static Lesson Create(Guid courseId, string title, int order)
    {
        return new Lesson
        {
            Id = Guid.NewGuid(),
            CourseId = courseId,
            Title = title,
            Order = order,
            IsCompleted = false
        };
    }

    public void MarkCompleted() => IsCompleted = true;
}