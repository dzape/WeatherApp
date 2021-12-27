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
using WeatherApp.Logic.Repository;
using WeatherApp.Logic.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly IUserRepository<User> _userRepo;
        private readonly IAssetsRepository<UserAssets> _assetsRepo;

        public UsersController(UserCrudService userService, IUserRepository<User> userRepo, IAssetsRepository<UserAssets> assetsRepo)
        {
            _userCrudService = userService;
            _userRepo = userRepo;
            _assetsRepo = assetsRepo;
        }

        [HttpPost]
        public async Task<Object> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_userCrudService.UserExist(user))
                {
                    await _userCrudService.AddUser(user);
                    await _assetsRepo.CreateAssets(user);
                    return Ok("User was created");
                }
                return Ok("Change your username.");
            }
            return Ok("Check your input and try again !");
        }

        [HttpPut]
        public object UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (!_userCrudService.UpdateUser(user).Equals(true))
                {
                    return Ok("User was updated");
                }
                return Ok("Username exist !");
            }
            return Ok("Check your input and try again !");
        }

        [HttpDelete]
        public object DeleteUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_userCrudService.DeleteUser(user).Equals(true))
                {
                    return Ok("User was deleted.");
                }
                return Unauthorized();
            }
            return Ok("Check your input and try again !");
        }
    }
}
