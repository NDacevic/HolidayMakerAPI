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
    public class UserHomesController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public UserHomesController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/UserHomes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Home>>> GetHome()
        {
            return await _context.Home.ToListAsync();
        }

        // GET: api/UserHomes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Home>>> GetHomes(int id)
        {
            var homes = await _context.Home.Where(h=>h.User.UserId==id).ToListAsync();

            if (homes == null)
            {
                return NotFound();
            }

            return Ok(homes);
        }

        // PUT: api/UserHomes/5
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

        // POST: api/UserHomes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Home>> PostHome(Home home)
        {
            _context.Home.Add(home);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHome", new { id = home.HomeId }, home);
        }

        // DELETE: api/UserHomes/5
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
