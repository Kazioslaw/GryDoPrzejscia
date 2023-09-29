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
    public class IndexModel : PageModel
    {
        private readonly GryDoPrzejscia.Data.ApplicationDbContext _context;

        public IndexModel(GryDoPrzejscia.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<GameList> GameList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GameList != null)
            {
                GameList = await _context.GameList.ToListAsync();
            }
        }
    }
}
