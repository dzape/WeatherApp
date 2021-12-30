﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionMvc.App.Utilities;
using System;
using System.Collections.Generic;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AssetsService _assetService;
        private readonly AuthService _authService;
        private readonly IMailSender _mailService;

        public UsersController(UserCrudService userService, IUserRepository<User> userRepo, AssetsService assetService, IMailSender mailService, AuthService authService)
        {
            _userCrudService = userService;
            _assetService = assetService;
            _authService = authService;
            _mailService = mailService;
        }

        [HttpGet, Route("get")]
        public IEnumerable<string> Get()
            => new string[] { "Hello", "Name" };



        [HttpPut]
        public object UpdateUser([FromBody] UserUpdate user)
        {
            if(ModelState.IsValid)
            {
                UserAssets info = HttpContext.Session.Get<UserAssets>("info");
                var updateUser = _userCrudService.GetUserByUsername(info.User.Username);

                var authUser = new UserLogin();
                authUser.Username = info.User.Username;
                authUser.Password = user.Password;
                if (_authService.Authenticate(authUser))
                {
                    updateUser.Username = user.Username;
                    
                    if (!_userCrudService.UpdateUser(updateUser).Equals(true))
                    {
                        return Ok("User was updated");
                    }
                    return Ok("Username exist !");
                }
                return Unauthorized();
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
