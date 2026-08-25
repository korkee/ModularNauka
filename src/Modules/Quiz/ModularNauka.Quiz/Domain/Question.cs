namespace ModularNauka.Quiz.Domain;

public class Question
{
    public Guid Id { get; private set; }
    public Guid QuizId { get; private set; }
    public string Text { get; private set; } = string.Empty;
    public string CorrectAnswer { get; private set; } = string.Empty;
    public List<string> Options { get; private set; } = new();

    public static Question Create(Guid quizId, string text, string correctAnswer, List<string> options)
    {
        return new Question
        {
            Id = Guid.NewGuid(),
            QuizId = quizId,
            Text = text,
            CorrectAnswer = correctAnswer,
            Options = options
        };
    }

    public bool IsCorrect(string answer) =>
        string.Equals(CorrectAnswer, answer, StringComparison.OrdinalIgnoreCase);
}