using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace GryDoPrzejscia.Pages.GamesList
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public IndexModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IList<GameList> GameList { get; set; } = default!;
		public string CurrentFilter { get; set; }


		public async Task OnGetAsync(string searchString)
		{
			CurrentFilter = searchString;
			if (GameList == null)
			{
				IQueryable<GameList> gamesList = _context.GameList.OrderByDescending(g => g.isPlayed)
									 .ThenBy(g => g.isFinished)
									 .ThenBy(g => g.Id);


				if (!String.IsNullOrEmpty(searchString))
				{
					if (Enum.TryParse(typeof(Launcher), searchString, out object launcher))
					{
						gamesList = gamesList.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.Launcher == (Launcher)launcher);
					}
					else
					{
						gamesList = gamesList.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
					}
				}


				GameList = await gamesList.ToListAsync();
			}
		}
	}
}
