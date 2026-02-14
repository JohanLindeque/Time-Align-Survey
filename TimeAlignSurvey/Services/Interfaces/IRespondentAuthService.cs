using System;
using TimeAlignSurvey.Models.Entities;

namespace TimeAlignSurvey.Services.Interfaces;

public interface IRespondentAuthService
{
    bool IsAuthenticated { get; }
    bool IsAdmin { get; }

    Respondent? CurrentUser { get; }
    
    Task<bool> LoginAsync(string userName, string password);
    void Logout();
}
