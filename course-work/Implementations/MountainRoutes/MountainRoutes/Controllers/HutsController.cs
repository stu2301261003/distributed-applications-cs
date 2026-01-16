using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MountainRoutes.Data;
using MountainRoutes.Models;

namespace MountainRoutes.Controllers
{
    public class HutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Huts – всички могат да виждат
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Huts.Include(h => h.Route);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Huts/Details/5 – всички могат да виждат
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hut = await _context.Huts
                .Include(h => h.Route)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hut == null) return NotFound();

            return View(hut);
        }

        // GET: Huts/Create – само за админ
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.MountainRoutes, "Id", "Name");
            return View();
        }

        // POST: Huts/Create – само за админ
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Capacity,HasRestaurant,Latitude,Longitude,RouteId")] Hut hut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.MountainRoutes, "Id", "Name", hut.RouteId);
            return View(hut);
        }

        // GET: Huts/Edit/5 – само за админ
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hut = await _context.Huts.FindAsync(id);
            if (hut == null) return NotFound();

            ViewData["RouteId"] = new SelectList(_context.MountainRoutes, "Id", "Name", hut.RouteId);
            return View(hut);
        }

        // POST: Huts/Edit/5 – само за админ
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Capacity,HasRestaurant,Latitude,Longitude,RouteId")] Hut hut)
        {
            if (id != hut.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Huts.Any(e => e.Id == hut.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.MountainRoutes, "Id", "Name", hut.RouteId);
            return View(hut);
        }

        // GET: Huts/Delete/5 – само за админ
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hut = await _context.Huts
                .Include(h => h.Route)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hut == null) return NotFound();

            return View(hut);
        }

        // POST: Huts/Delete/5 – само за админ
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hut = await _context.Huts.FindAsync(id);
            if (hut != null)
            {
                _context.Huts.Remove(hut);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
