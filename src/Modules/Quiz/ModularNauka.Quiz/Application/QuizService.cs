using Microsoft.EntityFrameworkCore;
using ModularNauka.Quiz.Infrastructure;
using ModularNauka.Shared.Contracts.Quiz;
using ModularNauka.Shared.Events;
using QuizEntity = ModularNauka.Quiz.Domain.Quiz;

namespace ModularNauka.Quiz.Application;

public class QuizService
{
    private readonly QuizDbContext _db;
    private readonly IEventBus _eventBus;

    public QuizService(QuizDbContext db, IEventBus eventBus)
    {
        _db = db;
        _eventBus = eventBus;
    }

    public async Task<QuizEntity> CreateQuizAsync(Guid lessonId, Guid courseId, string title, CancellationToken ct = default)
    {
        var quiz = QuizEntity.Create(lessonId, courseId, title);
        _db.Quizzes.Add(quiz);
        await _db.SaveChangesAsync(ct);
        return quiz;
    }

    public async Task AddQuestionAsync(Guid quizId, string text, string correctAnswer, List<string> options, CancellationToken ct = default)
    {
        var question = ModularNauka.Quiz.Domain.Question.Create(quizId, text, correctAnswer, options);
        _db.Questions.Add(question);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<SubmitResult> SubmitAnswersAsync(Guid quizId, Guid userId, Dictionary<Guid, string> answers, CancellationToken ct = default)
    {
        var quiz = await _db.Quizzes.FindAsync([quizId], ct);
        if (quiz is null) throw new InvalidOperationException("Quiz nie istnieje.");

        var questions = await _db.Questions
            .Where(q => q.QuizId == quizId)
            .ToListAsync(ct);

        var correct = questions.Count(q => answers.TryGetValue(q.Id, out var answer) && q.IsCorrect(answer));
        var total = questions.Count;

        await _eventBus.PublishAsync(new QuizSubmittedEvent(userId, quiz.LessonId, quiz.CourseId, correct, total), ct);

        return new SubmitResult(correct, total);
    }

    public async Task<List<QuizEntity>> GetQuizzesByLessonAsync(Guid lessonId, CancellationToken ct = default)
    {
        return await _db.Quizzes.Where(q => q.LessonId == lessonId).ToListAsync(ct);
    }
}

public record SubmitResult(int Correct, int Total)
{
    public double ScorePercent => Total == 0 ? 0 : Math.Round((double)Correct / Total * 100, 1);
}