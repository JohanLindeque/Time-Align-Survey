using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            // Step 1: Get all questions with their manager objectives
            var questions = await _context.Questions.Include(q => q.Objective).ToListAsync();

            // Step 2: Calculate average respondent weight for each question
            var respondentAverages = await _context
                .RespondentResults.GroupBy(r => r.QuestionId)
                .Select(g => new { QuestionId = g.Key, AvgWeight = g.Average(x => x.Weight) })
                .ToListAsync();

            // Step 3: Build results for each question
            var results = new List<ResultComparisonDto>();

            foreach (var question in questions)
            {
                var avgData = respondentAverages.FirstOrDefault(a => a.QuestionId == question.Id);

                decimal respondentWeight = avgData?.AvgWeight ?? 0;
                decimal managerWeight = question.Objective?.ExpectedWeight ?? 0;


                decimal gap = managerWeight - respondentWeight;

                // Calculate accuracy based on the gap
                decimal accuracy = CalculateAccuracy(respondentWeight, managerWeight, gap);

                results.Add(
                    new ResultComparisonDto
                    {
                        QuestionName = question.QuestionText,
                        RespondentAvgWeight = Math.Round(respondentWeight, 2),
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


    private decimal CalculateAccuracy(decimal respondentWeight, decimal managerWeight, decimal gap)
    {
       
        if (gap == 0)
        {
            return 100;
        }

        // Respondents are UNDER target (gap > 0, they spent LESS than expected)
        if (gap > 0 && managerWeight > 0)
        {
            // Formula: (respondentWeight * 100) / managerWeight
            // Example: respondent=5, manager=22, gap=17
            // (5 * 100) / 22 = 22.73%
            decimal accuracy = (respondentWeight * 100) / managerWeight;
            return accuracy;
        }

        // Respondents are OVER target (gap < 0, they spent MORE than expected)
        if (gap < 0 && respondentWeight > 0)
        {
            // Formula: (gap * 100) / respondentWeight
            // Example: respondent=10, manager=8, gap=-2
            // (-2 * 100) / 10 = -20%
            decimal accuracy = (gap * 100) / respondentWeight;
            return accuracy;
        }

        return 0;
    }
}
