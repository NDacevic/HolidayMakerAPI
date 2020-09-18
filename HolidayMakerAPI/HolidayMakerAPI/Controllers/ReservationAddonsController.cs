using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI;
using HolidayMakerAPI.Data;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationAddonsController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public ReservationAddonsController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/ReservationAddons
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Addon>>> GetReservationAddon(int id)
        {

            var addonId = _context.ReservationAddon.Where(a => a.ReservationId == id).ToList();
          

            var addons = _context.Addon.ToList();
            if(addonId.Count!=0)
            {
                foreach (var a in addonId)
                {
                    addons.Clear();
                    addons = _context.Addon.Where(b => b.AddonId == a.AddonId).ToList();

                }
            }
            else
            {
                addons.Clear();
            }
          
            return addons;
            //return await _context.ReservationAddon.ToListAsync();
        }

        // GET: api/ReservationAddons/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ReservationAddon>> GetReservationAddon(int id)
        //{
        //    var reservationAddon = await _context.ReservationAddon.FindAsync(id);

        //    if (reservationAddon == null)
        //    {
        //        return NotFound();
        //    }

        //    return reservationAddon;
        //}

        // PUT: api/ReservationAddons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservationAddon(int id, ReservationAddon reservationAddon)
        {
            if (id != reservationAddon.AddonId)
            {
                return BadRequest();
            }

            _context.Entry(reservationAddon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationAddonExists(id))
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

        // POST: api/ReservationAddons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ReservationAddon>> PostReservationAddon(ReservationAddon reservationAddon)
        {
            _context.ReservationAddon.Add(reservationAddon);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReservationAddonExists(reservationAddon.AddonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReservationAddon", new { id = reservationAddon.AddonId }, reservationAddon);
        }

        // DELETE: api/ReservationAddons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationAddon>> DeleteReservationAddon(int id)
        {
            var reservationAddon = await _context.ReservationAddon.FindAsync(id);
            if (reservationAddon == null)
            {
                return NotFound();
            }

            _context.ReservationAddon.Remove(reservationAddon);
            await _context.SaveChangesAsync();

            return reservationAddon;
        }

        private bool ReservationAddonExists(int id)
        {
            return _context.ReservationAddon.Any(e => e.AddonId == id);
        }
    }
}
