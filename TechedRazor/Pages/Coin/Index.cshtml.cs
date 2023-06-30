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
    public class IndexModel : PageModel
    {
        private readonly TechedRazor.Data.TechedRazorContext _context;

        public IndexModel(TechedRazor.Data.TechedRazorContext context)
        {
            _context = context;
        }

        public IList<CoinEntity> CoinEntity { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Coins != null)
            {
                CoinEntity = await _context.Coins.ToListAsync();
            }
        }
    }
}
