using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class RespondentResultRepository : IRespondentResultRepository
{
    public Task AddAsync(RespondentResult results)
    {
        throw new NotImplementedException();
    }

    public Task<List<RespondentResult>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasSubmittedAsync(int respondentId)
    {
        throw new NotImplementedException();
    }
}
