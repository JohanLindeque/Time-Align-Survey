using System;
using System.Net.Http.Headers;
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

    public async Task AddResponsesAsync(IEnumerable<RespondentResult> results)
    {
        await _context.RespondentResults.AddRangeAsync(results);

        await _context.SaveChangesAsync();
    }

    public async Task<List<RespondentResult>> GetAllAsync()
    {
        return await _context.RespondentResults.ToListAsync();
    }

    public async Task<bool> HasSubmittedAsync(int respondentId)
    {
        return await _context.RespondentResults.AnyAsync(r => r.RespondentId == respondentId);
    }
}
