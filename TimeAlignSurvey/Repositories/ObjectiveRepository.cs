using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey.Repositories;

public class ObjectiveRepository : IObjectiveRepository
{
    public Task<IEnumerable<Objective>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
