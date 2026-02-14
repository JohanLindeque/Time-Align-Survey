using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class QuestionRepository : IQuestionRepository
{
    public Task<IEnumerable<Question>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
