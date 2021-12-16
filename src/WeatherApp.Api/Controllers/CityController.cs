namespace CityApp.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;
    using WeatherApp.Api.Repository;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CityController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly ICityRepository _cityRepository;

        public CityController(UserDbContext context, ICityRepository cityRepository)
        {
            _context = context;
            _cityRepository = cityRepository;
        }

        // GET: api/City/test
        [HttpGet("{username}")]
        public IEnumerable GetFavouriteCity(string username)
        {
            var query = from r in _context.Cities
                        where r.UserUsername.Equals(username)
                        orderby r.Id
                        select r.Name;

            return query;
        }

        // POST: api/City
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityViewModel City)
        {
            if(!_cityRepository.DoesCityExist(City.Name, City.UserUsername))
            {
                var city = new City();

                city.Name = City.Name;
                city.UserUsername = City.UserUsername;
                
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();

                return Ok(city.Name);
            }

            return null;
        }

        // DELETE: api/City/5
        [HttpDelete]
        public async Task<ActionResult<City>> DeleteCity(CityViewModel City)
        {
            var city = from r in _context.Cities
                       where r.UserUsername.Equals(City.UserUsername) && r.Name.Equals(City.Name)
                       select r.Id;

            var delete = await _context.Cities.FindAsync(city.FirstOrDefault());

            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(delete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
