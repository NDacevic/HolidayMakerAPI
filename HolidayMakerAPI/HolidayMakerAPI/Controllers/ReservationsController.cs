using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI.Data;
using HolidayMakerAPI.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.JsonPatch;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public ReservationsController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            try
            {
                return await _context.Reservation.ToListAsync();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }


        /// <summary>
        /// Patches a reservation with the supplied information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jsonPatchStudent"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchReservation(int id, [FromBody] JsonPatchDocument<Reservation> jsonPatchReservation)
        {

            _context.Database.BeginTransaction();
            //Find reservation using id
            Reservation updateReservation = await _context.Reservation.FirstOrDefaultAsync(x => x.ReservationId == id);
            var addonlist = await _context.ReservationAddon.Where(x => x.ReservationId == id).ToListAsync();
            updateReservation.ReservationAddons = new List<ReservationAddon>();
           
    
            //List<ReservationAddon> tempList = new List<ReservationAddon>();

            if (updateReservation == null)
                return NotFound();

            //Add changes
            jsonPatchReservation.ApplyTo(updateReservation, ModelState);

            if (updateReservation.Addons.Count != 0)
            {
                foreach (var r in updateReservation.Addons)
                {
                    ReservationAddon resAddon = new ReservationAddon();
                    //tempList.Add(new ReservationAddon() { AddonId = r.AddonId, ReservationId = updateReservation.ReservationId });
                      resAddon.AddonId = r.AddonId;
                      resAddon.ReservationId = id;
                      updateReservation.ReservationAddons.Add(resAddon);
                }
                //     updateReservation.ReservationAddons.AddRange(tempList);
                //((tempList.Where(x => addonlist.Any(y => y.ReservationId == x.ReservationId))).ToList()).ForEach(x => tempList.Remove(x));

            }
           else
           {
                foreach(ReservationAddon r in addonlist)
                {
                   _context.ReservationAddon.Remove(r);
                }
           }


            //Error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(updateReservation))
                return BadRequest(ModelState);

            _context.Update(updateReservation);
            await _context.SaveChangesAsync();

            _context.Database.CommitTransaction();
            return Ok();


       
        }

        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Database.BeginTransaction();
     
      
            _context.Reservation.Add(reservation);
            await _context.SaveChangesAsync();

            //A list of addons is passed inside the Reservation object.
            //Go through these and add the Id of the addon and reservation to the ReservationAddon table
            foreach (Addon ra in reservation.Addons)
            {
                ReservationAddon tempRa = new ReservationAddon();
                tempRa.ReservationId = reservation.ReservationId;
                tempRa.AddonId = ra.AddonId;
                _context.ReservationAddon.Add(tempRa);
            }
            await _context.SaveChangesAsync();

            _context.Database.CommitTransaction();
            
            return Ok();
            
            //_context.Reservation.Add(reservation);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetReservation", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/Reservations/5
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
