using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Services.Interfaces;

public interface ISurveyService
{
    Task<IEnumerable<Question>> GetAllQuestionsAsync();

    Task<bool> HasUserSubmittedAsync(int respondentId);

    Task SubmitSurveyAsync(int respondentId, Dictionary<int, int> answers);

}
