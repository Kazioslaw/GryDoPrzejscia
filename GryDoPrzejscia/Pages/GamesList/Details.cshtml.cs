using System;
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
    public class DetailsModel : PageModel
    {
        private readonly GryDoPrzejscia.Data.ApplicationDbContext _context;

        public DetailsModel(GryDoPrzejscia.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
