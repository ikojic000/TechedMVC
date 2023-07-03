using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;
using TechedRazor.Models.ViewModel;
using TechedRazor.Services.CoinServices;

namespace TechedRazor.Pages.Coin
{
    public class EditModel : PageModel
    {
        private readonly IDatabaseService _databaseService;

        public EditModel(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [BindProperty]
        public CoinViewModel CoinViewModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) { return NotFound(); }

            var coinViewModel = await _databaseService.GetCoinFromDatabaseAsync(id);

            if (coinViewModel != null)
            {
                CoinViewModel = coinViewModel;
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _databaseService.UpdateCoinFromDatabase(CoinViewModel);

            return RedirectToPage("./Index");
        }


    }
}
