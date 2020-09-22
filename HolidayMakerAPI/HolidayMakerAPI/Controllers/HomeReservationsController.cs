using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI.Data;
using HolidayMakerAPI.Models;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeReservationsController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public HomeReservationsController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/HomeReservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            return await _context.Reservation.ToListAsync();
        }

        // GET: api/HomeReservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var res = await _context.Reservation.Where(r => r.HomeId == id).Select(r => r).ToListAsync();
            List<Reservation> reservations = new List<Reservation>();

            if (res == null)
            {
                return NotFound();
            }
            else
            {
                foreach(var r in res)
                {
                    reservations.Add(r);
                }
            }

            return Ok(reservations);
        }


// PUT: api/HomeReservations/5
// To protect from overposting attacks, enable the specific properties you want to bind to, for
// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
[HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/HomeReservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/HomeReservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationId == id);
        }
    }
}
