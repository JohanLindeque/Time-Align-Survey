using System;
using System.ComponentModel.DataAnnotations;

namespace TimeAlignSurvey.Models.Entities;

public class Respondent
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    public ICollection<RespondentResult> Results { get; set; }
}
