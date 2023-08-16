using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GryDoPrzejscia.Pages.GamesList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public GameList GameList { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            GameList = _db.GameList.Find(id);
        }

        public async Task<IActionResult> OnPost([FromForm] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var gameListFromDb = await _db.GameList.FindAsync(gameList.Id);
                    gameListFromDb.isPlayed = gameList.isPlayed;
                    gameListFromDb.isFinished = gameList.isFinished;

                    await _db.SaveChangesAsync();

                    TempData["success"] = "Gra edytowana pomyœlnie";
                    return RedirectToPage("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["error"] = "Wyst¹pi³ b³¹d podczas edycji gry.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
