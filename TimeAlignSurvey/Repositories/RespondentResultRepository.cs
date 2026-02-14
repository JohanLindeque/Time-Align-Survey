using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class RespondentResultRepository : IRespondentResultRepository
{
    private readonly SurveyAppDbContext _context;

    public RespondentResultRepository(SurveyAppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RespondentResult result)
    {
        await _context.RespondentResults.AddAsync(result);

        await _context.SaveChangesAsync();
    }

    public async Task<List<RespondentResult>> GetAllAsync()
    {
        var allResults = await _context.RespondentResults.ToListAsync();

        return allResults;
    }

    public async Task<bool> HasSubmittedAsync(int respondentId)
    {
        var hasSubmitted = await _context.RespondentResults.AnyAsync(r =>
            r.RespondentId == respondentId
        );

        return hasSubmitted;
    }
}
