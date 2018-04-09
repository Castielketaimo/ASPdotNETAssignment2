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
using Microsoft.AspNetCore.Identity;

namespace Lmyc_server.Controllers.MVC
{
    [Authorize (Policy = "RequireLogin")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

            // GET: Reservations
            public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservation.Include(r => r.Boat).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Boat)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["BoatId"] = new SelectList(_context.Boat, "BoatId", "BoatName");
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "UserName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,StartDateTime,EndDateTime,CreatedBy,BoatId")] Reservation newReservation)
        {
            ViewData["BoatId"] = new SelectList(_context.Boat, "BoatId", "BoatName", newReservation.BoatId);
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", newReservation.CreatedBy);
            if (ModelState.IsValid)
            {
                var reservations = await _context.Reservation.ToListAsync();
                ////.Where(r => r.Boat == newReservation.Boat && r.StartDateTime.Day == newReservation.StartDateTime.Day);

                //foreach (var reservation in reservations)
                //{
                //    if (newReservation.StartDateTime < reservation.EndDateTime
                //        || (newReservation.StartDateTime < reservation.StartDateTime && newReservation.EndDateTime < reservation.EndDateTime))
                //    {
                //        return View(newReservation);
                //    }
                //}
                var user = await _userManager.GetUserAsync(User);
                newReservation.Boat = _context.Boat.SingleOrDefault(b => b.BoatId == newReservation.BoatId);
                newReservation.User = user;
                _context.Add(newReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(newReservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["BoatId"] = new SelectList(_context.Boat, "BoatId", "BoatName", reservation.BoatId);
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", reservation.CreatedBy);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,StartDateTime,EndDateTime,CreatedBy,BoatId")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["BoatId"] = new SelectList(_context.Boat, "BoatId", "BoatName", reservation.BoatId);
            ViewData["CreatedBy"] = new SelectList(_context.ApplicationUser, "Id", "Id", reservation.CreatedBy);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Boat)
                .Include(r => r.User)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationId == id);
        }
    }
}
