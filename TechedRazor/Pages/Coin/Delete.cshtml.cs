using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin;

public class DeleteModel : PageModel
{
    private readonly IDatabaseService _databaseService;

    public DeleteModel(IDatabaseService databaseService)
    {
        _databaseService = databaseService;
    }

    [BindProperty] public CoinViewModel CoinViewModel { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null) return NotFound();

        var coinViewModel = await _databaseService.GetCoinFromDatabaseAsync(id);
        if (coinViewModel == null)
            return NotFound();
        CoinViewModel = coinViewModel;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null) return NotFound();

        await _databaseService.DeleteCoinFromDatabaseAsync(id);

        return RedirectToPage("./Index");
    }
}