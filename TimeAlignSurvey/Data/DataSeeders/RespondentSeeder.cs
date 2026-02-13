using System;
using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Data.DataSeeders;

public class RespondentSeeder
{
    public static async Task SeedRespondentsAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<SurveyAppDbContext>();

        if (await context.Respondents.AnyAsync())
            return;

        var respondents = new List<Respondent>
        {
            new Respondent
            {
                UserName = "u1",
                FirstName = "John",
                LastName = "Smith",
                Password = "password",
            },
            new Respondent
            {
                UserName = "u2",
                FirstName = "Susan",
                LastName = "Birnam",
                Password = "password",
            },
            new Respondent
            {
                UserName = "u3",
                FirstName = "Carter",
                LastName = "Flamings",
                Password = "password",
            },
            new Respondent
            {
                UserName = "u4",
                FirstName = "Elrond",
                LastName = "Raven",
                Password = "password",
            },
            new Respondent
            {
                UserName = "u5",
                FirstName = "Frodo",
                LastName = "Smitherns",
                Password = "password",
            },
        };

        foreach (var respondent in respondents)
        {
            context.Respondents.Add(respondent);
        }

        await context.SaveChangesAsync();
    }
}
