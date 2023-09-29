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
            if (ModelState.IsValid)
            {
                try
                {
                    var gameListFromDb = await _db.GameList.FindAsync(GameList.Id);

                    if (gameListFromDb != null)
                    {
                        _db.GameList.Remove(gameListFromDb);
                        await _db.SaveChangesAsync();

                        TempData["success"] = "Gra usuni�ta pomy�lnie";
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        TempData["error"] = "Nie mo�na znale�� gry do usuni�cia.";
                        return RedirectToPage("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Wyst�pi� b��d podczas usuwania gry: " + ex.Message;
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
