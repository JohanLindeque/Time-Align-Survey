using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TimeAlignSurvey.Models.DTO;
using TimeAlignSurvey.Services.Interfaces;

namespace TimeAlignSurvey.Components.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly ISurveyResultService _resultService;
        private readonly IRespondentAuthService _authService;

        public ResultsModel(ISurveyResultService resultService, IRespondentAuthService authService)
        {
            _resultService = resultService;
            _authService = authService;
        }

        public IEnumerable<ResultComparisonDto> Results { get; set; } =
            new List<ResultComparisonDto>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!_authService.IsAuthenticated || !_authService.IsAdmin)
                return RedirectToPage("/Login");

            Results = await _resultService.GetResultsAsync();
            return Page();
        }
    }
}
