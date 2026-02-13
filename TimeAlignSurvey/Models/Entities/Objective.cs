using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeAlignSurvey.Models.Entities;

public class Objective
{
    public int Id { get; set; }

    [Required]
    public int QuestionId { get; set; }

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    public decimal ExpectedWeight { get; set; }

    public Question Question { get; set; }
}
