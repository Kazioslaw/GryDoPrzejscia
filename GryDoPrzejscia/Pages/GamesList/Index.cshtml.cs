using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GryDoPrzejscia.Pages.GamesList
{
	public class IndexModel : PageModel
	{
		private readonly ApplicationDbContext _context;
		private readonly IConfiguration _configuration;

		public IndexModel(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
			_configuration = configuration;
		}

		public IList<GameList> GameList { get; set; } = default!;
		public string CurrentFilter { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }

		public async Task OnGetAsync(int pageIndex, string q)
		{

			PageSize = 10;
			PageIndex = (pageIndex < 1) ? 1 : pageIndex;
			CurrentFilter = q;
			var skip = (PageIndex - 1) * PageSize;

			if (GameList == null)
			{
				IQueryable<GameList> gamesList = _context.GameList.OrderByDescending(g => g.isPlayed)
									 .ThenBy(g => g.isFinished)
									 .ThenBy(g => g.Id);
				object launcher;

				if (!String.IsNullOrEmpty(q))
				{
					if (Enum.TryParse(typeof(Launcher), q, out launcher!))
					{
						gamesList = gamesList.Where(s => s.Name.ToLower().Contains(q.ToLower()) || s.Launcher == (Launcher)launcher);
					}
					else
					{
						gamesList = gamesList.Where(s => s.Name.ToLower().Contains(q.ToLower()));
					}
				}
				TotalPages = (int)Math.Ceiling(decimal.Divide(gamesList.Count(), PageSize));
				GameList = await gamesList.Skip(skip).Take(PageSize).ToListAsync();
			}
		}
	}
}
