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
            return await _context.Reservation.ToListAsync();
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
           


            //Find reservation using id
            Reservation updateReservation = await _context.Reservation.FirstOrDefaultAsync(x => x.ReservationId == id);
            ReservationAddon resA = new ReservationAddon();
            if (updateReservation == null)
                return NotFound();

            //Add changes
            jsonPatchReservation.ApplyTo(updateReservation, ModelState);
          
            //Error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!TryValidateModel(updateReservation))
                return BadRequest(ModelState);

            //Update object and save changes
            _context.Update(updateReservation);
            foreach(var ra in updateReservation.Addons)
            {
                resA.AddonId = ra.AddonId;
                resA.ReservationId = updateReservation.ReservationId;
            }
            _context.ReservationAddon.Add(resA);//TODO: This have to be fixed.
            await _context.SaveChangesAsync();

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
