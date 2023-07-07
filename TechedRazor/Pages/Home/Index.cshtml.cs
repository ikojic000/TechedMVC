using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.ApiServices;

namespace TechedRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IPublicApiService _publicApiService;

        public IndexModel(IPublicApiService publicApiService)
        {
            _publicApiService = publicApiService;
        }

        public IList<Models.ViewModel.CoinDTO> CoinDTOList { get; set; }

        public async Task OnGet()
        {
            CoinDTOList = await _publicApiService.GetCoinList();
        }
    }
}