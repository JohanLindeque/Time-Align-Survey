using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class RespondentRepository : IRespondentRepository
{
    public Task<Respondent> AuthenticateUserLoginAsync(Respondent respondent)
    {
        throw new NotImplementedException();
    }

    public Task<Respondent> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Respondent>> GetByUsernameAsync(Respondent respondent)
    {
        throw new NotImplementedException();
    }
}
