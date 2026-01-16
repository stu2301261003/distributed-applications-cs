using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MountainRoutes.Data;
using MountainRoutes.Models;

namespace MountainRoutes.Controllers
{
    public class RoutesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Всички могат да виждат списъка
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var routes = _context.MountainRoutes.Include(r => r.Mountain);
            return View(await routes.ToListAsync());
        }

        // Всички могат да виждат подробности
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var route = await _context.MountainRoutes
                .Include(r => r.Mountain)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (route == null) return NotFound();

            return View(route);
        }

        // Създаване – само за админ
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["MountainId"] = new SelectList(_context.Mountains, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(MountainRoute route)
        {
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MountainId"] = new SelectList(_context.Mountains, "Id", "Name", route.MountainId);
            return View(route);
        }

        // Редакция – само за админ
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var route = await _context.MountainRoutes.FindAsync(id);
            if (route == null) return NotFound();

            ViewData["MountainId"] = new SelectList(_context.Mountains, "Id", "Name", route.MountainId);
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, MountainRoute route)
        {
            if (id != route.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MountainRoutes.Any(e => e.Id == route.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MountainId"] = new SelectList(_context.Mountains, "Id", "Name", route.MountainId);
            return View(route);
        }

        // Изтриване – само за админ
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var route = await _context.MountainRoutes
                .Include(r => r.Mountain)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (route == null) return NotFound();

            return View(route);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.MountainRoutes.FindAsync(id);
            if (route != null)
            {
                _context.MountainRoutes.Remove(route);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
