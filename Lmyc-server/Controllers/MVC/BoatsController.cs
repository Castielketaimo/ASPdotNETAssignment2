using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lmyc_server.Data;
using Lmyc_server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lmyc_server.Controllers.MVC
{
    [Authorize (Policy="RequireLogin")]
    public class BoatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Boats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Boat.Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Boats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boat
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BoatId == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        [Authorize (Policy = "RequireAdmin")]
        // GET: Boats/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BoatName,Picture,LengthInFeet,Make,Year")] Boat boat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", boat.CreatedBy);
            return View(boat);
        }

        // GET: Boats/Edit/5
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boat.SingleOrDefaultAsync(m => m.BoatId == id);
            if (boat == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", boat.CreatedBy);
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Edit(int id, [Bind("BoatName,Picture,LengthInFeet,Make,Year")] Boat boat)
        {
            if (id != boat.BoatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoatExists(boat.BoatId))
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
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", boat.CreatedBy);
            return View(boat);
        }

        // GET: Boats/Delete/5
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boat = await _context.Boat
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.BoatId == id);
            if (boat == null)
            {
                return NotFound();
            }

            return View(boat);
        }

        // POST: Boats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boat = await _context.Boat.SingleOrDefaultAsync(m => m.BoatId == id);
            _context.Boat.Remove(boat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoatExists(int id)
        {
            return _context.Boat.Any(e => e.BoatId == id);
        }
    }
}
