using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public GameList GameList { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            GameList = _db.GameList.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var gameFromDb = _db.GameList.Find(GameList.Id);
            if (gameFromDb != null)
            {
                _db.GameList.Remove(GameList);
                await _db.SaveChangesAsync();
                TempData["success"] = "Gra usuniêta pomyœlnie";
                return RedirectToPage("Index");
            }
            return Page();
        }

    }
}
