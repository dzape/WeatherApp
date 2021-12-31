using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Services;
using WeatherApp.Logic.Utilities;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CityController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AssetsService _assetService;
        private readonly AuthService _authService;
        private readonly CityService _cityService;

        public CityController(CityService cityService, UserCrudService userService, IUserRepository<User> userRepo, AssetsService assetService, AuthService authService)
        {
            _cityService = cityService;
            _userCrudService = userService;
            _assetService = assetService;
            _authService = authService;
        }

        [HttpGet, Route("test")]
        public IEnumerable<string> Test()
            => new string[] { "Hello", "Perro" };

        [HttpGet, Route("cities")]
        public IEnumerable<City> GetCities()
        {
            var user = _userCrudService.GetUserByUsername(HttpContext.User.Identity.Name);
            return _cityService.GetCitiesByUsername(user.Username);
        }

        [HttpPost]
        public async Task<ActionResult<City>> AddCity(City city)
        {
            var curentUser = HttpContext.User;
            var user = _userCrudService.GetUserByUsername(curentUser.Identity.Name);
            if (_cityService.CityMatch(city))
            {
                city.User = user;
                await _cityService.AddCity(city);
            }

            return Ok("City added to favourites");
        }

        [HttpDelete]
        public async Task<ActionResult<City>> DeleteCity(City city)
        {
            if(city != null)
            {
                _cityService.DeleteCity(city);
            }

            return Ok("City was deleted.");
        }
    }
}
