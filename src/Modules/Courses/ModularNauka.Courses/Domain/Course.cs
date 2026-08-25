namespace ModularNauka.Courses.Domain;

public class Course
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public static Course Create(string title, string description)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description
        };
    }
}