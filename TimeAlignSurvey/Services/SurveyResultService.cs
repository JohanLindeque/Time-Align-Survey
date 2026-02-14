using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Models.DTO;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Services;

public class SurveyResultService : ISurveyResultService
{
    private readonly SurveyAppDbContext _context;
    private readonly ILogger<SurveyResultService> _logger;

    public SurveyResultService(SurveyAppDbContext context, ILogger<SurveyResultService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ResultComparisonDto>> GetResultsAsync()
    {
        try
        {
            // get questions
            var questions = await _context.Questions.Include(q => q.Objective).ToListAsync();

            // avg weight per question
            var respondentAverages = await _context
                .RespondentResults.GroupBy(r => r.QuestionId)
                .Select(g => new { QuestionId = g.Key, AvgWeight = g.Average(x => x.Weight) })
                .ToListAsync();

            var results = new List<ResultComparisonDto>();

            foreach (var question in questions)
            {
                var avgData = respondentAverages.FirstOrDefault(a => a.QuestionId == question.Id);

                var respondentAvg = avgData?.AvgWeight ?? 0;
                var managerWeight = question.Objective?.ExpectedWeight ?? 0;
                var gap = respondentAvg - managerWeight;

                // accuracy
                decimal accuracy = 0;
                if (managerWeight > 0)
                {
                    accuracy = 100 - (Math.Abs(gap) / managerWeight * 100);
                    accuracy = Math.Max(0, accuracy);
                }
                else if (respondentAvg == 0)
                {
                    accuracy = 100;
                }

                results.Add(
                    new ResultComparisonDto
                    {
                        QuestionName = question.QuestionText,
                        RespondentAvgWeight = Math.Round(respondentAvg, 2),
                        ExpectationGap = Math.Round(gap, 2),
                        Accuracy = Math.Round(accuracy, 2),
                        ManagerWeight = managerWeight,
                    }
                );
            }

            return results;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating survey results.");
            throw;
        }
    }
}
