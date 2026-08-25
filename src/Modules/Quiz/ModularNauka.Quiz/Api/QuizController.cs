using Microsoft.AspNetCore.Mvc;
using ModularNauka.Quiz.Application;

namespace ModularNauka.Quiz.Api;

[ApiController]
[Route("api/quizzes")]
public class QuizController : ControllerBase
{
    private readonly QuizService _quizService;

    public QuizController(QuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuiz([FromBody] CreateQuizRequest request, CancellationToken ct)
    {
        var quiz = await _quizService.CreateQuizAsync(request.LessonId, request.CourseId, request.Title, ct);
        return Ok(new { quiz.Id, quiz.Title });
    }

    [HttpPost("{quizId:guid}/questions")]
    public async Task<IActionResult> AddQuestion(Guid quizId, [FromBody] AddQuestionRequest request, CancellationToken ct)
    {
        await _quizService.AddQuestionAsync(quizId, request.Text, request.CorrectAnswer, request.Options, ct);
        return Ok();
    }

    [HttpPost("{quizId:guid}/submit")]
    public async Task<IActionResult> Submit(Guid quizId, [FromBody] SubmitQuizRequest request, CancellationToken ct)
    {
        var result = await _quizService.SubmitAnswersAsync(quizId, request.UserId, request.Answers, ct);
        return Ok(new
        {
            result.Correct,
            result.Total,
            result.ScorePercent
        });
    }

    [HttpGet("{quizId:guid}/questions")]
    public async Task<IActionResult> GetQuestions(Guid quizId, CancellationToken ct)
    {
        var questions = await _quizService.GetQuestionsAsync(quizId, ct);
        return Ok(questions);
    }

    [HttpGet("lesson/{lessonId:guid}")]
    public async Task<IActionResult> GetByLesson(Guid lessonId, CancellationToken ct)
    {
        var quizzes = await _quizService.GetQuizzesByLessonAsync(lessonId, ct);
        return Ok(quizzes.Select(q => new { q.Id, q.Title, q.LessonId }));
    }
}

public record CreateQuizRequest(Guid LessonId, Guid CourseId, string Title);
public record AddQuestionRequest(string Text, string CorrectAnswer, List<string> Options);
public record SubmitQuizRequest(Guid UserId, Dictionary<Guid, string> Answers);