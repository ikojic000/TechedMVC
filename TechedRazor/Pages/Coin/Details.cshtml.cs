using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin
{
    public class DetailsModel : PageModel
    {

        private readonly IDatabaseService _databaseService;

        public DetailsModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

      public CoinViewModel CoinViewModel{ get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coinViewModel = await _databaseService.GetCoinFromDatabaseAsync(id);
            if (coinViewModel == null)
            {
                return NotFound();
            }
            else 
            {
                CoinViewModel = coinViewModel;
            }
            return Page();
        }
    }
}
