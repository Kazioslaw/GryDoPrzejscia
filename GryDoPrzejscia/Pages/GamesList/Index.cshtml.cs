using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GryDoPrzejscia.Pages.GamesList;
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<GameList> GamesList { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public void OnGet()
    {
        GamesList = _db.GameList;
    }
}
