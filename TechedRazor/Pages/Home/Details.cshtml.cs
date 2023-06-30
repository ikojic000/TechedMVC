using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.ApiServices;

namespace TechedRazor.Pages.Home
{
    public class DetailsModel : PageModel
    {
        private readonly PublicApiService _publicApiService;

        public DetailsModel(PublicApiService publicApiService)
        {
            _publicApiService = publicApiService;
        }

        public CoinViewModel CoinModel { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string Coin_id)
        {
            if (Coin_id == null)
            {
                return NotFound();
            }

            IList<CoinViewModel> coinList = await _publicApiService.GetCoinList();

            var coin = coinList.FirstOrDefault(i => i.Id == Coin_id);

            CoinModel = coin;

            return Page();

        }
    }
}
