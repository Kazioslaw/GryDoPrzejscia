using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Swashbuckle.AspNetCore.Annotations;

namespace GryDoPrzejscia.Pages.GamesList
{
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		[SwaggerSchema("Dane gry do dodania")]
		public GameList GameList { get; set; } = default!;

		[SwaggerOperation(
			Summary = "Dodaj nową grę",
			Description = "Dodaje nową grę do listy gier.",
			OperationId = "DodajGra",
			Tags = new[] { "Gry" })]
		[SwaggerResponse(200, "Grę pomyślnie dodano.")]
		[SwaggerResponse(400, "Błąd walidacji danych.")]
		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.GameList == null || GameList == null)
			{
				return Page();
			}

			bool gameExist = _context.GameList.Any(g => g.Name.ToLower() == GameList.Name.ToLower());

			if (gameExist)
			{
				ModelState.AddModelError("GameList.Title", "Gra o podanym tytule jest już w bazie");
				return Page();
			}

			_context.GameList.Add(GameList);
			await _context.SaveChangesAsync();
			TempData["success"] = "Poprawnie dodano Grę";
			TempData["IsChanged"] = true;
			return RedirectToPage("./Index");
		}
	}
}
