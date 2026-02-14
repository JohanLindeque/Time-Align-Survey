using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class ObjectiveRepository : IObjectiveRepository
{
    private readonly SurveyAppDbContext _context;

    public ObjectiveRepository(SurveyAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Objective>> GetAllAsync()
    {
        var objectivesInDb = await _context.Objectives.ToListAsync();

        return objectivesInDb;
    }
}
