using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Data.Helpers;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Services;
using WeatherApp.Logic.Utilities;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AssetsService _assetService;
        private readonly IMailSender _mailService;

        public UsersController(UserCrudService userService, IUserRepository<User> userRepo, AssetsService assetService, IMailSender mailService)
        {
            _userCrudService = userService;
            _assetService = assetService;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<Object> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_userCrudService.EmailMatch(user))
                {
                    await _userCrudService.AddUser(user);
                    await _assetService.CreateAssets(user);

                    _mailService.SendEmailAsync(user.Email);
                    return Ok("User was created");
                }
                return Ok("Email exist.");
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
