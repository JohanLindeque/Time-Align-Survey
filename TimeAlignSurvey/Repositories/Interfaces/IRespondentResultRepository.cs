using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Repositories.Interfaces;

public interface IRespondentResultRepository
{
    Task AddResponsesAsync(IEnumerable<RespondentResult> results);
    Task<bool> HasSubmittedAsync(int respondentId);
    Task<List<RespondentResult>> GetAllAsync();
}
