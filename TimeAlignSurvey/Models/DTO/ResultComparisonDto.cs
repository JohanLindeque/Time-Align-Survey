using System;

namespace TimeAlignSurvey.Models.DTO;

public class ResultComparisonDto
{
    public string QuestionName { get; set; }
    public decimal RespondentAvgWeight { get; set; }
    public decimal ExpectationGap { get; set; }
    public decimal Accuracy { get; set; }
    public decimal ManagerWeight { get; set; }

}