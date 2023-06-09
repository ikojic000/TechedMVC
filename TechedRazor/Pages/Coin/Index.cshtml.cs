﻿using Microsoft.AspNetCore.Mvc.RazorPages;
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


        public void OnGet()
        {
        }
    }
}
