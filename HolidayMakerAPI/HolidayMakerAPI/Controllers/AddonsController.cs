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
    public class AddonsController : ControllerBase
    {
        private readonly HolidayMakerAPIContext _context;

        public AddonsController(HolidayMakerAPIContext context)
        {
            _context = context;
        }

        // GET: api/Addons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addon>>> GetAddon()
        {
            return await _context.Addon.ToListAsync();
        }

        // GET: api/Addons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Addon>> GetAddon(int id)
        {
            var addon = await _context.Addon.FindAsync(id);

            if (addon == null)
            {
                return NotFound();
            }

            return addon;
        }

        // PUT: api/Addons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddon(int id, Addon addon)
        {
            if (id != addon.AddonId)
            {
                return BadRequest();
            }

            _context.Entry(addon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddonExists(id))
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

        // POST: api/Addons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Addon>> PostAddon(Addon addon)
        {
            _context.Addon.Add(addon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddon", new { id = addon.AddonId }, addon);
        }

        // DELETE: api/Addons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Addon>> DeleteAddon(int id)
        {
            var addon = await _context.Addon.FindAsync(id);
            if (addon == null)
            {
                return NotFound();
            }

            _context.Addon.Remove(addon);
            await _context.SaveChangesAsync();

            return addon;
        }

        private bool AddonExists(int id)
        {
            return _context.Addon.Any(e => e.AddonId == id);
        }
    }
}
