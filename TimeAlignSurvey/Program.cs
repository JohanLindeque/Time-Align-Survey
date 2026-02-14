using Microsoft.EntityFrameworkCore;
using TimeAlignSurvey.Components;
using TimeAlignSurvey.Data;
using TimeAlignSurvey.Data.DataSeeders;
using TimeAlignSurvey.Repositories;
using TimeAlignSurvey.Repositories.Interfaces;

namespace TimeAlignSurvey;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // db context
        builder.Services.AddDbContext<SurveyAppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("BookNestDB"));
        });

        // Add services to the container.
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();

        builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
        builder.Services.AddScoped<IObjectiveRepository, ObjectiveRepository>();
        builder.Services.AddScoped<IRespondentRepository, RespondentRepository>();
        builder.Services.AddScoped<IRespondentResultRepository, RespondentResultRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Seed data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                QuestionSeeder.SeedQuestionsAsync(services).Wait();
                RespondentSeeder.SeedRespondentsAsync(services).Wait();
                ObjectiveSeeder.SeedObjectivesAsync(services).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        app.Run();
    }
}
