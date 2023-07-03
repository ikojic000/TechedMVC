using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.ApiServices;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IPublicApiService _publicApiService;
        private readonly IDatabaseService _databaseService;

        public DetailsModel(IPublicApiService publicApiService, IDatabaseService databaseService)
        {
            _publicApiService = publicApiService;
            _databaseService = databaseService;
        }

        [BindProperty]
        public CoinViewModel CoinModel { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string Coin_id)
        {
            if (Coin_id == null) { return NotFound(); }

            IList<CoinViewModel> coinList = await _publicApiService.GetCoinList();

            var coin = coinList.FirstOrDefault(i => i.Id == Coin_id);

            CoinModel = coin;

            return Page();

        }

        public async Task<IActionResult> OnPostAsync(string Coin_id)
        {
            if (Coin_id == null) { return NotFound(); }

            IList<CoinViewModel> coinList = await _publicApiService.GetCoinList();

            var coin = coinList.FirstOrDefault(i => i.Id == Coin_id);

            _databaseService.SaveToDatabase(coin);

            return RedirectToPage("../Coin/Index");

        }
    }
}
