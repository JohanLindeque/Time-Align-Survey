using System;
using System.ComponentModel.DataAnnotations;

namespace TimeAlignSurvey.Models.Entities;

public class Question
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string QuestionText { get; set; }

    public Objective Objective { get; set; }

    public ICollection<RespondentResult> Results { get; set; }
}
