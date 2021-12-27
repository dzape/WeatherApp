using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Repository;
using WeatherApp.Logic.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AuthService _authService;

        private readonly IUserRepository<User> _userRepo;
        private readonly IAssetsRepository<UserAssets> _assetsRepo;

        public AuthController(  UserCrudService userService, 
                                AuthService authService,
                                IUserRepository<User> userRepo, 
                                IAssetsRepository<UserAssets> assetsRepo)
        {
            _userCrudService = userService;
            _authService = authService;
            _userRepo = userRepo;
            _assetsRepo = assetsRepo;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_authService.Authenticate(user))
                {
                    var token = _authService.GenerateToken(user);
                    return Ok(new { Token = token });
                }
            }
            return null;
        }

    }
}
