using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Data.DataSeeders;

public class QuestionSeeder
{
    public static async Task SeedQuestionsAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SurveyAppDbContext>();

        if (await context.Questions.AnyAsync())
            return;

        var questions = new List<Question>
        {
            new Question { QuestionText = "Internal Meetings" },
            new Question { QuestionText = "Client Meetings" },
            new Question { QuestionText = "Emails & Phone / Skype calls" },
            new Question { QuestionText = "Research" },
            new Question { QuestionText = "DB Design" },
            new Question { QuestionText = "Architecture Design and Planning" },
            new Question { QuestionText = "Back-end Development" },
            new Question { QuestionText = "Front-end Development" },
            new Question { QuestionText = "Integration" },
            new Question { QuestionText = "Testing" },
        };

        foreach (var question in questions)
        {
            context.Questions.Add(question);
        }

        await context.SaveChangesAsync();
    }
}
