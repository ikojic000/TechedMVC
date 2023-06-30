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
    public class DeleteModel : PageModel
    {
        private readonly TechedRazor.Data.TechedRazorContext _context;

        public DeleteModel(TechedRazor.Data.TechedRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Coins == null)
            {
                return NotFound();
            }
            var coinentity = await _context.Coins.FindAsync(id);

            if (coinentity != null)
            {
                CoinEntity = coinentity;
                _context.Coins.Remove(CoinEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
