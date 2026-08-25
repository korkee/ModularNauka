namespace ModularNauka.Quiz.Domain;

public class Quiz
{
    public Guid Id { get; private set; }
    public Guid LessonId { get; private set; }
    public Guid CourseId { get; private set; }
    public string Title { get; private set; } = string.Empty;

    public static Quiz Create(Guid lessonId, Guid courseId, string title)
    {
        return new Quiz
        {
            Id = Guid.NewGuid(),
            LessonId = lessonId,
            CourseId = courseId,
            Title = title
        };
    }
}