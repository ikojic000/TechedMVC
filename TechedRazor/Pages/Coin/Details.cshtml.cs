using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;

namespace TechedRazor.Pages.Coin
{
    public class DetailsModel : PageModel
    {
        private readonly TechedRazor.Data.TechedRazorContext _context;

        public DetailsModel(TechedRazor.Data.TechedRazorContext context)
        {
            _context = context;
        }

      public CoinEntity CoinEntity { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }

            var coinentity = await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);
            if (coinentity == null)
            {
                return NotFound();
            }
            else 
            {
                CoinEntity = coinentity;
            }
            return Page();
        }
    }
}
