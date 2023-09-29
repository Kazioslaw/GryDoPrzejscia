using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class CreateModel : PageModel
    {
        private readonly GryDoPrzejscia.Data.ApplicationDbContext _context;

        public CreateModel(GryDoPrzejscia.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public GameList GameList { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.GameList == null || GameList == null)
            {
                return Page();
            }

            _context.GameList.Add(GameList);
            await _context.SaveChangesAsync();
            TempData["success"] = "Poprawnie dodano Grę";

            return RedirectToPage("./Index");
        }
    }
}
