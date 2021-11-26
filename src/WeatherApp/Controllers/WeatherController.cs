using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LoginForm.Data;
using WeatherApp.Data;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly AccountDbContext _context;

        public WeatherController(AccountDbContext context)
        {
            _context = context;
        }

        // GET: api/Weather
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weather>>> GetWeathers()
        {
            return await _context.Weathers.ToListAsync();
        }

        // GET: api/Weather/5
        [HttpGet("{id}")]
        public IEnumerable<Weather> GetWeather(int id)
        {
            var query = from r in _context.Weathers
                        where r.AccountId.Equals(id)
                        orderby r.CityName
                        select r;

            return query;
        }

        // PUT: api/Weather/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeather(int id, Weather weather)
        {
            if (id != weather.CityId)
            {
                return BadRequest();
            }

            _context.Entry(weather).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(id))
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

        // POST: api/Weather
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weather>> PostWeather(Weather weather)
        {
            _context.Weathers.Add(weather);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeather", new { id = weather.CityId }, weather);
        }

        // DELETE: api/Weather/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeather(int id)
        {
            var weather = await _context.Weathers.FindAsync(id);
            if (weather == null)
            {
                return NotFound();
            }

            _context.Weathers.Remove(weather);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherExists(int id)
        {
            return _context.Weathers.Any(e => e.CityId == id);
        }
    }
}
