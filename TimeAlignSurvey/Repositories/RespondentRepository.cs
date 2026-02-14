using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class RespondentRepository : IRespondentRepository
{
    private readonly SurveyAppDbContext _context;

    public RespondentRepository(SurveyAppDbContext context)
    {
        _context = context;
    }

    public async Task<Respondent?> GetByUsernameAsync(string username)
    {
        return await _context.Respondents.FirstOrDefaultAsync(r => r.UserName == username);
    }
}
