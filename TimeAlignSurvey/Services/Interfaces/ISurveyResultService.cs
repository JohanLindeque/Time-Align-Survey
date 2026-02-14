using System;
using TimeAlignSurvey.Models.DTO;

namespace TimeAlignSurvey.Services.Interfaces;

public interface ISurveyResultService
{
    Task<IEnumerable<ResultComparisonDto>> GetResultsAsync();
}
