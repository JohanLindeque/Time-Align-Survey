using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Components.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IRespondentAuthService _authService;

        public LoginModel(IRespondentAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() => _authService.Logout();

        public async Task<IActionResult> OnPostAsync()
        {
            if (await _authService.LoginAsync(Username, Password))
            {
                return _authService.IsAdmin
                    ? RedirectToPage("/Results")
                    : RedirectToPage("/Survey");
            }

            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
