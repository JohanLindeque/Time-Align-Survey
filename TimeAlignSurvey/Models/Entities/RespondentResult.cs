using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAlignSurvey.Models.Entities;

public class RespondentResult
{
    public int Id { get; set; }

    [Required]
    public int RespondentId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal Weight { get; set; }

    public Respondent Respondent { get; set; }
    public Question Question { get; set; }
}
