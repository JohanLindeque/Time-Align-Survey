using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Repositories.Interfaces;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> GetAllAsync();
}
