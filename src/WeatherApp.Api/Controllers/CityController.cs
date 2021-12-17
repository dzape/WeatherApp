namespace CityApp.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserRepository _userRepository;

        public CityController(UserDbContext context, ICityRepository cityRepository, IUserRepository userRepository)
        {
            _context = context;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{username}")]
        public IEnumerable GetFavouriteCity(string username)
        {
            var user = _userRepository.GetUser(username);
            
            var query = from r in _context.Cities
                        where r.UserId.Equals(user.Id)
                        orderby r.Id
                        select r.Name;

            return query;
        }

         [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityViewModel City)
        {
            if(!_cityRepository.DoesCityExist(City.Name, City.UserUsername))
            {
                var city = new City();

                var user = _userRepository.GetUser(City.UserUsername);
                city.Name = City.Name;
                city.UserUsername = user.Username; 
                city.UserId = user.Id;

                _context.Cities.Add(city);
                await _context.SaveChangesAsync();

                return Ok(city.Name);
            }

            return null;
        }

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
