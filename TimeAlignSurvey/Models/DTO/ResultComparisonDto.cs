using System;

namespace TimeAlignSurvey.Models.DTO;

public class ResultComparisonDto
{
public string QuestionName { get; set; }

    public decimal ActualAverage { get; set; }

    public decimal Objective { get; set; }

    public decimal Gap { get; set; }

    public decimal Accuracy { get; set; }
}
