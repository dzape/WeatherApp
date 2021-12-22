namespace CityApp.Controllers
{
    using System.Collections;
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

        [HttpGet]
        public IEnumerable GetFavouriteCity()
        {
            var curentUser = HttpContext.User;

            var user = _userRepository.GetUser(curentUser.Identity.Name);

            var query = _cityRepository.GetCities(user);

            return query;
        }

         [HttpPost]
        public async Task<ActionResult<City>> PostCity(CityViewModel City)
        {
            if(!_cityRepository.CityMatch(City))
            {
                var user = _userRepository.GetUser(HttpContext.User.Identity.Name);

                var city = new City();

                city.Name = City.Name;
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
            var user = _userRepository.GetUser(HttpContext.User.Identity.Name);

            try
            {
                var city = _cityRepository.GetCity(City);

                if (city == null)
                {
                    return NotFound();
                }

                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();

                return Ok("City Deleted.");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
