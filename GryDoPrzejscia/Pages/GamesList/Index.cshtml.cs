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
		private static IList<GameList>? _cachedGameList;
		public async Task OnGetAsync()
		{
			if (_cachedGameList == null || TempData.ContainsKey("IsChanged") && (bool)TempData["IsChanged"])
			{
				_cachedGameList = await _context.GameList.OrderByDescending(g => g.isPlayed).ThenBy(g => g.isFinished).ThenBy(g => g.Id).ToListAsync();
				TempData["IsChanged"] = false;
			}
			GameList = _cachedGameList;
		}
	}
}
