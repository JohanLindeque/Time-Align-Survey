using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Services;

public class SurveyService : ISurveyService
{
    private readonly IQuestionRepository _questionRepo;
    private readonly IRespondentResultRepository _resultRepo;
    private readonly ILogger<SurveyService> _logger;

    public SurveyService(
        IQuestionRepository questionRepo,
        IRespondentResultRepository resultRepo,
        ILogger<SurveyService> logger
    )
    {
        _questionRepo = questionRepo;
        _resultRepo = resultRepo;
        _logger = logger;
    }

    public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
    {
        var questions = await _questionRepo.GetAllAsync();

        return questions.OrderBy(q => Guid.NewGuid()).ToList();
    }

    public async Task<bool> HasUserSubmittedAsync(int respondentId)
    {
        return await _resultRepo.HasSubmittedAsync(respondentId);
    }

    public async Task SubmitSurveyAsync(int respondentId, Dictionary<int, int> answers)
    {
        try
        {
            if (await _resultRepo.HasSubmittedAsync(respondentId))
                throw new InvalidOperationException("User has already submitted survey.");

            var results = answers.Select(a => new RespondentResult
            {
                RespondentId = respondentId,
                QuestionId = a.Key,
                Weight = a.Value,
            });

            await _resultRepo.AddResponsesAsync(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error submitting survey for Respondent {RespondentId}",
                respondentId
            );

            return;
        }
    }
}
