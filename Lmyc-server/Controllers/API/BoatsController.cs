using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lmyc_server.Data;
using Lmyc_server.Models;
using Microsoft.AspNetCore.Authorization;
using AspNet.Security.OAuth.Validation;

namespace Lmyc_server.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Boats")]
    [Authorize(Policy = "RequireLogin", AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme)]
    public class BoatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Boats
        [HttpGet]
        public IEnumerable<Boat> GetBoat()
        {
            return _context.Boat;
        }

        // GET: api/Boats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var boat = await _context.Boat.SingleOrDefaultAsync(m => m.BoatId == id);

            if (boat == null)
            {
                return NotFound();
            }

            return Ok(boat);
        }

        // PUT: api/Boats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoat([FromRoute] int id, [FromBody] Boat boat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != boat.BoatId)
            {
                return BadRequest();
            }

            _context.Entry(boat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoatExists(id))
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

        // POST: api/Boats
        [HttpPost]
        public async Task<IActionResult> PostBoat([FromBody] Boat boat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Boat.Add(boat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoat", new { id = boat.BoatId }, boat);
        }

        // DELETE: api/Boats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoat([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var boat = await _context.Boat.SingleOrDefaultAsync(m => m.BoatId == id);
            if (boat == null)
            {
                return NotFound();
            }

            _context.Boat.Remove(boat);
            await _context.SaveChangesAsync();

            return Ok(boat);
        }

        private bool BoatExists(int id)
        {
            return _context.Boat.Any(e => e.BoatId == id);
        }
    }
}