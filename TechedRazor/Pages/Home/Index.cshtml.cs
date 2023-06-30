using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.ApiServices;

namespace TechedRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PublicApiService _publicApiService;

        public IndexModel(PublicApiService publicApiService)
        {
            _publicApiService = publicApiService;
        }

        public IList<CoinViewModel> CoinViewModels { get; set; }

        public async Task OnGet()
        {
            CoinViewModels = await _publicApiService.GetCoinList();
        }
    }
}