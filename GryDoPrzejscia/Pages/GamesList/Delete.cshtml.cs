using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerSchema("Dane gry do usunięcia")]
        public GameList GameList { get; set; } = default!;

        [SwaggerOperation(
            Summary = "Usuń grę",
            Description = "Usuwa grę z listy gier na podstawie identyfikatora.",
            OperationId = "UsunGra",
            Tags = new[] { "Gry" })]
        [SwaggerResponse(200, "Grę pomyślnie usunięto.")]
        [SwaggerResponse(404, "Brak gry o podanym identyfikatorze.")]
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

        [SwaggerOperation(
    Summary = "Potwierdź usunięcie gry",
    Description = "Potwierdza usunięcie gry na podstawie identyfikatora.",
    OperationId = "PotwierdzUsuniecieGry",
    Tags = new[] { "Gry" })]
        [SwaggerResponse(200, "Grę pomyślnie usunięto.")]
        [SwaggerResponse(404, "Brak gry o podanym identyfikatorze.")]
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
