using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public GameList GameList { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _db.GameList.AddAsync(GameList);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }

    }
}
