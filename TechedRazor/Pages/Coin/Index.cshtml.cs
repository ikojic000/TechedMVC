using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
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

        public IList<CoinDTO> CoinViewModels { get; set; } = default!;
        public string NameSort { get; set; }
        public string SearchString { get; set; }

        public async Task OnGet(string sortOrder, string search)
        {
            /*
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            SearchString = search;

            CoinViewModels = await _databaseService.GetAllFromDatabaseAsync(NameSort, SearchString);
            */
        }
    }
}
