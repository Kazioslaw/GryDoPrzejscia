using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GryDoPrzejscia.Data;
using GryDoPrzejscia.Model;

namespace GryDoPrzejscia.Controler
{
    public class GameListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameLists
        public async Task<IActionResult> Index()
        {
              return _context.GameList != null ? 
                          View(await _context.GameList.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.GameList'  is null.");
        }

        // GET: GameLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GameList == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameList == null)
            {
                return NotFound();
            }

            return View(gameList);
        }

        // GET: GameLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Launcher,isPlayed,isFinished")] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameList);
        }

        // GET: GameLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GameList == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList.FindAsync(id);
            if (gameList == null)
            {
                return NotFound();
            }
            return View(gameList);
        }

        // POST: GameLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Launcher,isPlayed,isFinished")] GameList gameList)
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
                    if (!GameListExists(gameList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gameList);
        }

        // GET: GameLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GameList == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameList
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameList == null)
            {
                return NotFound();
            }

            return View(gameList);
        }

        // POST: GameLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GameList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GameList'  is null.");
            }
            var gameList = await _context.GameList.FindAsync(id);
            if (gameList != null)
            {
                _context.GameList.Remove(gameList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameListExists(int id)
        {
          return (_context.GameList?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
