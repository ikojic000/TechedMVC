using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin
{
    public class EditModel : PageModel
    {
        private readonly IDatabaseService _databaseService;
        private readonly ICoinValidationService _coinValidationService;

        public EditModel(IDatabaseService databaseService, ICoinValidationService coinValidationService)
        {
            _databaseService = databaseService;
            _coinValidationService = coinValidationService;
        }

        [BindProperty]
        public CoinDTO CoinDTO { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) { return NotFound(); }

            var coinDTO = await _databaseService.GetCoinFromDatabaseAsync(id);

            if (coinDTO != null)
            {
                CoinDTO = coinDTO;
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _coinValidationService.IsCoinDTOValid(CoinDTO);
            if (result is ValidationResult)
            {
                ValidationResult validationResult = (ValidationResult)result;
                validationResult.AddToModelState(ModelState, nameof(CoinDTO));

                return Page();
            }

            await _databaseService.UpdateCoinFromDatabase(CoinDTO);

            return RedirectToPage("./Index");
        }


    }
}
