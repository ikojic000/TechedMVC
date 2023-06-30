using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseService _databaseService;

        public IndexModel(DatabaseService databaseService)
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
