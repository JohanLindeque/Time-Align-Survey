using System;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Repositories.Interfaces;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Services;

public class RespondentAuthService : IRespondentAuthService
{
    private readonly IRespondentRepository _respondentRepo;
    private readonly ILogger<RespondentAuthService> _logger;

    public RespondentAuthService(
        IRespondentRepository respondentRepo,
        ILogger<RespondentAuthService> logger
    )
    {
        _respondentRepo = respondentRepo;
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
            var user = await _respondentRepo.GetByUsernameAsync(userName);

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
