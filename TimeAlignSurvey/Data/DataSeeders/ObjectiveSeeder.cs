using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Data.DataSeeders;

public class ObjectiveSeeder
{
    public static async Task SeedObjectivesAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SurveyAppDbContext>();

        if (await context.Objectives.AnyAsync())
            return;

        var questions = await context.Questions.ToListAsync();

        if (!questions.Any())
        {
            throw new InvalidOperationException(
                "Questions must be seeded before objectives. Run QuestionSeeder first."
            );
        }

        var objectives = new List<Objective>
        {
            new Objective { QuestionId = questions[0].Id, ExpectedWeight = 8 }, // Internal Meetings
            new Objective { QuestionId = questions[1].Id, ExpectedWeight = 8 }, // Client Meetings
            new Objective { QuestionId = questions[2].Id, ExpectedWeight = 5 }, // Emails & Phone / Skype calls
            new Objective { QuestionId = questions[3].Id, ExpectedWeight = 5 }, // Research
            new Objective { QuestionId = questions[4].Id, ExpectedWeight = 2 }, // DB Design
            new Objective { QuestionId = questions[5].Id, ExpectedWeight = 5 }, // Architecture Design and Planning
            new Objective { QuestionId = questions[6].Id, ExpectedWeight = 30 }, // Back-end Development
            new Objective { QuestionId = questions[7].Id, ExpectedWeight = 22 }, // Front-end Development
            new Objective { QuestionId = questions[8].Id, ExpectedWeight = 5 }, // Integration
            new Objective { QuestionId = questions[9].Id, ExpectedWeight = 10 }, // Testing
        };

        foreach (var objective in objectives)
        {
            context.Objectives.Add(objective);
        }

        await context.SaveChangesAsync();
    }
}
