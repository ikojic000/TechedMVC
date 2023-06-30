﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechedRazor.Data;
using TechedRazor.Models.Domain;

namespace TechedRazor.Pages.Coin
{
    public class EditModel : PageModel
    {
        private readonly TechedRazor.Data.TechedRazorContext _context;

        public EditModel(TechedRazor.Data.TechedRazorContext context)
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

            var coinentity =  await _context.Coins.FirstOrDefaultAsync(m => m.Id == id);
            if (coinentity == null)
            {
                return NotFound();
            }
            CoinEntity = coinentity;
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

            _context.Attach(CoinEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoinEntityExists(CoinEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CoinEntityExists(int id)
        {
          return (_context.Coins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}