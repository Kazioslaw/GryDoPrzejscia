using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class EditModel : PageModel
    {
        private readonly GryDoPrzejscia.Data.ApplicationDbContext _context;

        public EditModel(GryDoPrzejscia.Data.ApplicationDbContext context)
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

            var gamelist =  await _context.GameList.FirstOrDefaultAsync(m => m.Id == id);
            if (gamelist == null)
            {
                return NotFound();
            }
            GameList = gamelist;
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

            _context.Attach(GameList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameListExists(GameList.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData["success"] = "Poprawnie edytowano grę";

            return RedirectToPage("./Index");
        }

        private bool GameListExists(int id)
        {
          return (_context.GameList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
