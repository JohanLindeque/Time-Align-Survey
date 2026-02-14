using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Services;

public class RespondentAuthService : IRespondentAuthService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RespondentAuthService> _logger;

    public RespondentAuthService(
        IServiceScopeFactory scopeFactory,
        ILogger<RespondentAuthService> logger
    )
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Respondent? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser != null;
    public bool IsAdmin => CurrentUser?.UserName == "admin";

    public async Task<bool> LoginAsync(string userName, string password)
    {
        // Admin login
        if (userName == "admin" && password == "P@ssw0rd")
        {
            CurrentUser = new Respondent { Id = 6, UserName = "admin" };
            return true;
        }

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var respondentRepo = scope.ServiceProvider.GetRequiredService<IRespondentRepository>();

            var user = await respondentRepo.GetByUsernameAsync(userName);

            if (user == null)
                return false;

            if (user.Password != password)
                return false;

            CurrentUser = user;
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error occurred during login attempt for username {Username}",
                userName
            );

            return false;
        }
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
