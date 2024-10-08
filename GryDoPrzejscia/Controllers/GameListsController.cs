using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GryDoPrzejscia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Pobierz listę gier",
            Description = "Pobierz listę wszystkich gier.",
            OperationId = "GetGameLists")]
        public async Task<IActionResult> GetGameLists()
        {
            var gameLists = await _context.GameList.ToListAsync();
            return Ok(gameLists);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Pobierz szczegóły gry",
            Description = "Pobierz szczegóły gry na podstawie identyfikatora.",
            OperationId = "GetGameList")]
        [SwaggerResponse(200, "Grę pomyślnie znaleziono.")]
        [SwaggerResponse(404, "Brak gry o podanym identyfikatorze.")]
        public async Task<IActionResult> GetGameList(int id)
        {
            var gameList = await _context.GameList.FindAsync(id);
            if (gameList == null)
            {
                return NotFound();
            }
            return Ok(gameList);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Dodaj nową grę",
            Description = "Dodaj nową grę do listy gier.",
            OperationId = "AddGameList")]
        [SwaggerResponse(201, "Grę pomyślnie dodano.")]
        [SwaggerResponse(400, "Błąd walidacji danych.")]
        public async Task<IActionResult> AddGameList([FromBody] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameList);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGameList", new { id = gameList.Id }, gameList);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edytuj grę",
            Description = "Edytuj grę na podstawie identyfikatora.",
            OperationId = "EditGameList")]
        [SwaggerResponse(204, "Grę pomyślnie zaktualizowano.")]
        [SwaggerResponse(400, "Błąd walidacji danych.")]
        [SwaggerResponse(404, "Brak gry o podanym identyfikatorze.")]
        public async Task<IActionResult> EditGameList(int id, [FromBody] GameList gameList)
        {
            if (id != gameList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameListExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Usuń grę",
            Description = "Usuń grę na podstawie identyfikatora.",
            OperationId = "DeleteGameList")]
        [SwaggerResponse(204, "Grę pomyślnie usunięto.")]
        [SwaggerResponse(404, "Brak gry o podanym identyfikatorze.")]
        public async Task<IActionResult> DeleteGameList(int id)
        {
            var gameList = await _context.GameList.FindAsync(id);
            if (gameList == null)
            {
                return NotFound();
            }

            _context.GameList.Remove(gameList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameListExists(int id)
        {
            return (_context.GameList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
