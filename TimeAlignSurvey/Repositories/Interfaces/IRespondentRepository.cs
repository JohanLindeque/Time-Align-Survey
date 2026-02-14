using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Repositories.Interfaces;

public interface IRespondentRepository
{
    Task<Respondent?> GetByUsernameAsync(string userName);
    
}
