﻿using Microsoft.AspNetCore.Http;
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
    [Route("api/[controller]")]
    [ApiController]
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
                try
                {
                    await _userCrudService.AddUser(user);
                    await _assetsRepo.CreateAssets(user);
                    return Ok("User was created");
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        [HttpPut]
        public object UpdateUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                // TODO : if Authorized 
                try
                {
                    _userCrudService.UpdateUser(user);
                    return Ok("User was updated");
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}