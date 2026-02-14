using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly SurveyAppDbContext _context;

    public QuestionRepository(SurveyAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Question>> GetAllAsync()
    {
        var questionsInDb = await _context.Questions.ToListAsync();

        return questionsInDb;
    }
}
