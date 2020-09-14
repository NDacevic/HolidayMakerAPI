using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HolidayMakerAPI;
using HolidayMakerAPI.Data;
using HolidayMakerAPI.DTO;
using HolidayMakerAPI.Models;

namespace HolidayMakerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchResultsController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public SearchResultsController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/SearchResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHome()
        {
            return await _context.Home.ToListAsync();
        }

        // GET: api/SearchResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Home>> GetHome(int id)
        {
            var home = await _context.Home.FindAsync(id);

            if (home == null)
            {
                return NotFound();
            }

            return home;
        }

        // PUT: api/SearchResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHome(int id, Home home)
        {
            if (id != home.HomeId)
            {
                return BadRequest();
            }

            _context.Entry(home).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeExists(id))
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

        // POST: api/SearchResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.

        //POST used as GET to be able to pass in an object
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Home>>> PostHome(SearchParameterDto searchParameters)
        {
            //Get all homes within the correct location and that has more beds in total than NumberOfGuests. Save to list.
            List<Home> selectedHomes = await _context.Home.
                Where(h => h.Location == searchParameters.Location &&
                h.NumberOfBeds >= searchParameters.NumberOfGuests).ToListAsync();

            //Loop through all relevant homes and check for conflicting reservations
            for (int i = 0; i < selectedHomes.Count; i++)
            {
                //Recreate the list each iteration since only reservations per active home is relevant
                List<Reservation> reservationsPerHome = new List<Reservation>();

                //Get all reservations for the current iteration's home object where the reservations enddate is larger than the searches start date (only reservations that hasn't already passed)
                reservationsPerHome = await _context.Reservation.Where(r => r.Home.HomeId == selectedHomes[i].HomeId && r.EndDate>=searchParameters.StartDate).ToListAsync();

                for (int j = 0; j < reservationsPerHome.Count; j++)
                {
                    //if this home's current iteration's reservation's StartDate is BEFORE the Search's EndDate AND AFTER the Search's StartDate - then the Home is not available when wanted
                    if ((reservationsPerHome[j].StartDate < searchParameters.EndDate && reservationsPerHome[j].StartDate > searchParameters.StartDate) ||
                        //OR this home's current iteration's reservation's EndDate is AFTER the Search's StartDate AND BEFORE the Search's EndDate - then the Home is not available when wanted
                        (reservationsPerHome[j].EndDate>searchParameters.StartDate && reservationsPerHome[j].EndDate<searchParameters.EndDate))
                    {
                        //The home is not available when wanted, remove from list
                        selectedHomes.Remove(selectedHomes[i]);
                        //go back one incrementation since one home has been removed
                        i--;
                        //break out of Reservation-loop, the home is not available anyway
                        break;
                    }
                }
            }

            return selectedHomes;
        }

        // DELETE: api/SearchResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Home>> DeleteHome(int id)
        {
            var home = await _context.Home.FindAsync(id);
            if (home == null)
            {
                return NotFound();
            }

            _context.Home.Remove(home);
            await _context.SaveChangesAsync();

            return home;
        }

        private bool HomeExists(int id)
        {
            return _context.Home.Any(e => e.HomeId == id);
        }
    }
}
