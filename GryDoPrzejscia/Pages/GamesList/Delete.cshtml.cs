﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class DeleteModel : PageModel
    {
        private readonly GryDoPrzejscia.Data.ApplicationDbContext _context;

        public DeleteModel(GryDoPrzejscia.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public GameList GameList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GameList == null)
            {
                return NotFound();
            }

            var gamelist = await _context.GameList.FirstOrDefaultAsync(m => m.Id == id);

            if (gamelist == null)
            {
                return NotFound();
            }
            else 
            {
                GameList = gamelist;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GameList == null)
            {
                return NotFound();
            }
            var gamelist = await _context.GameList.FindAsync(id);

            if (gamelist != null)
            {
                GameList = gamelist;
                _context.GameList.Remove(GameList);
                await _context.SaveChangesAsync();
            }
            TempData["success"] = "Poprawnie usunięto Grę";
            return RedirectToPage("./Index");
        }
    }
}
