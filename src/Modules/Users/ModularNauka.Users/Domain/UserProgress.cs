namespace ModularNauka.Users.Domain;

public class UserProgress
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public int CompletedLessons { get; private set; }

    public static UserProgress Start(Guid userId, Guid courseId)
    {
        return new UserProgress
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CourseId = courseId,
            CompletedLessons = 0
        };
    }

    public void IncrementLesson() => CompletedLessons++;
}