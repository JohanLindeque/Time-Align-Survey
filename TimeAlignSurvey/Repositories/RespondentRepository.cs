using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http.HttpResults;
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

    public async Task<Respondent> GetByIdAsync(int id)
    {
        var respondent = await _context.Respondents.FirstOrDefaultAsync(r => r.Id == id);

        if (respondent == null)
            throw new KeyNotFoundException($"Respondent with id {id} was not found.");

        return respondent;
    }

    public async Task<Respondent?> GetByUsernameAsync(string username)
    {
        var respondent = await _context.Respondents.FirstOrDefaultAsync(r =>
            r.UserName == username
        );

        return respondent;
    }
}
