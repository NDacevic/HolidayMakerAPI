using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI;
using HolidayMakerAPI.Data;
using Microsoft.AspNetCore.JsonPatch;

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
            List<Addon> tempList = new List<Addon>();
            
          
            if(addonId.Count!=0)
            {
                foreach (var a in addonId)
                {

                    var addons = _context.Addon.Where(x=>x.AddonId==a.AddonId).ToList();
                   
                    foreach(var x in addons)
                    {
                        tempList.Add(x);
                    }
                }
             
            }
            else
            {
                tempList.Clear();
            }
          
            return tempList;
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
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchReservationAddon(int id, [FromBody] JsonPatchDocument<ReservationAddon> jsonPatchReservation)
        {

            //Find reservation using id
            ReservationAddon updateReservationAddon = await _context.ReservationAddon.FirstOrDefaultAsync(x => x.ReservationId == id);

     
            if (updateReservationAddon == null)
                return NotFound();

            //Add changes
            jsonPatchReservation.ApplyTo(updateReservationAddon, ModelState);

            //Error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(updateReservationAddon))
                return BadRequest(ModelState);


            //Update object and save changes
            _context.Update(updateReservationAddon);

            await _context.SaveChangesAsync();

            return Ok();



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
