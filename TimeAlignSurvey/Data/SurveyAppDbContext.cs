using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Data;

public class SurveyAppDbContext : DbContext
{
    public SurveyAppDbContext(DbContextOptions<SurveyAppDbContext> options)
        : base(options) { }

    public DbSet<Respondent> Respondents { get; set; }
    public DbSet<Objective> Objectives { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<RespondentResult> RespondentResults { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RespondentResult>()
            .HasIndex(r => new { r.RespondentId, r.QuestionId })
            .IsUnique();
    }

}
