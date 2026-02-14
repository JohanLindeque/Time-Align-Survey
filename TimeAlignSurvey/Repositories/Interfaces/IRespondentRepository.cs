using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Repositories.Interfaces;

public interface IRespondentRepository
{
    Task<Respondent> AuthenticateUserLoginAsync(Respondent respondent);
    Task<IEnumerable<Respondent>> GetByUsernameAsync(Respondent respondent);
    Task<Respondent> GetByIdAsync(int id);
}
