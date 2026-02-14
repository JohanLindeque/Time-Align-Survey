using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeAlignSurvey.Models.Entities;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Components.Pages
{
    public class SurveyModel : PageModel
    {
        private readonly ISurveyService _surveyService;
        private readonly IRespondentAuthService _authService;

        public SurveyModel(ISurveyService surveyService, IRespondentAuthService authService)
        {
            _surveyService = surveyService;
            _authService = authService;
        }

        public List<Question> Questions { get; set; } = new();

        [BindProperty]
        public List<int> QuestionIds { get; set; } = new();

        [BindProperty]
        public List<int> Weights { get; set; } = new();

        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authService.IsAuthenticated || _authService.IsAdmin)
                return RedirectToPage("/Login");

            Questions = (await _surveyService.GetAllQuestionsAsync())
                .OrderBy(q => Guid.NewGuid())
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!_authService.IsAuthenticated || _authService.IsAdmin)
                return RedirectToPage("/Login");

            if (QuestionIds.Count != Weights.Count)
            {
                ErrorMessage = "Invalid submission.";
                return await OnGetAsync();
            }

            if (Weights.Sum() != 100)
            {
                ErrorMessage = "Total weight must equal 100%.";
                return await OnGetAsync();
            }

            if (await _surveyService.HasUserSubmittedAsync(_authService.CurrentUser.Id))
                return RedirectToPage("/SubmissionConfirmation");

            var answers = QuestionIds
                .Zip(Weights, (q, w) => new { q, w })
                .ToDictionary(x => x.q, x => x.w);

            await _surveyService.SubmitSurveyAsync(_authService.CurrentUser.Id, answers);

            return RedirectToPage("/SubmissionConfirmation");
        }
    }
}
