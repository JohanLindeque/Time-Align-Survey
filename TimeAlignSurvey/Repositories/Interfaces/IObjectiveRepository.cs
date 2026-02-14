using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Repositories.Interfaces;

public interface IObjectiveRepository
{
    Task<IEnumerable<Objective>> GetAllAsync();
}
