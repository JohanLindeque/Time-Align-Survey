using System;
using System.Net.Http.Headers;
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
        return await _context.Questions.ToListAsync();

    }
}
