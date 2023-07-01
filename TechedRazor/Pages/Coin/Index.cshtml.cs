using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin
{
    public class IndexModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public IndexModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        public IList<CoinViewModel> CoinViewModels { get; set; } = default!;

        public async Task OnGet()
        {
            CoinViewModels = await _databaseService.GetAllFromDatabaseAsync();
        }
    }
}
