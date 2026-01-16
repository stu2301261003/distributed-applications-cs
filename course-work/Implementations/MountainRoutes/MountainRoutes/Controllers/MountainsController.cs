using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MountainRoutes.Data;
using MountainRoutes.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace MountainRoutes.Controllers
{
    public class MountainsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MountainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mountains
        [AllowAnonymous] // достъп за гости
        public async Task<IActionResult> Index(string? sortOrder, string? searchString, int? page)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["HeightSortParm"] = sortOrder == "Height" ? "height_desc" : "Height";
            ViewData["CurrentFilter"] = searchString;

            var mountains = from m in _context.Mountains
                            select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                mountains = mountains.Where(m =>
                    (m.Name != null && m.Name.Contains(searchString)) ||
                    (m.Location != null && m.Location.Contains(searchString))
                );
            }

            mountains = sortOrder switch
            {
                "name_desc" => mountains.OrderByDescending(m => m.Name),
                "Height" => mountains.OrderBy(m => m.Height),
                "height_desc" => mountains.OrderByDescending(m => m.Height),
                _ => mountains.OrderBy(m => m.Name)
            };

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var pagedMountains = mountains.AsNoTracking().ToPagedList(pageNumber, pageSize);
            return View(pagedMountains);
        }

        // GET: Mountains/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mountain = await _context.Mountains
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mountain == null) return NotFound();

            return View(mountain);
        }

        // GET: Mountains/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mountains/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Height,FirstAscent,Description")] Mountain mountain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mountain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mountain);
        }

        // GET: Mountains/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var mountain = await _context.Mountains.FindAsync(id);
            if (mountain == null) return NotFound();

            return View(mountain);
        }

        // POST: Mountains/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Height,FirstAscent,Description")] Mountain mountain)
        {
            if (id != mountain.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mountain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MountainExists(mountain.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mountain);
        }

        // GET: Mountains/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mountain = await _context.Mountains
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mountain == null) return NotFound();

            return View(mountain);
        }

        // POST: Mountains/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mountain = await _context.Mountains.FindAsync(id);
            if (mountain != null)
            {
                _context.Mountains.Remove(mountain);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MountainExists(int id)
        {
            return _context.Mountains.Any(e => e.Id == id);
        }
    }
}
