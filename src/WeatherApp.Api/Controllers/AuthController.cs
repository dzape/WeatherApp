using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SessionMvc.App.Utilities;
using System;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AssetsService _assetsService;
        private readonly AuthService _authService;

        public AuthController( UserCrudService userService,
                               AssetsService assetsService,
                               AuthService authService )
        {
            _userCrudService = userService;
            _assetsService = assetsService;
            _authService = authService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_assetsService.IsMailVerified(user))
                {
                    if (_authService.Authenticate(user))
                    {
                        var asset = _assetsService.GetAssets(user);
                        HttpContext.Session.Set<UserAssets>("info", asset);
                        var token = _authService.GenerateToken(user);
                        return Ok(new { Token = token });
                    }
                }
            }
            return Ok("Check your inputs.");
        }

        [HttpPut("{guid}"), Route("activation")]
        public IActionResult ActivateAccount(Guid guid)
        {
            if(guid != null)
            {
                _assetsService.ActivateMail(guid);
                return Ok("Your account is verified :)");
            }
            return Ok("Verification Failed");
        }

        [HttpGet, Route("fetch-from-session")]
        public IActionResult FetchFromSession()
        {
            try
            {
                UserAssets info = HttpContext.Session.Get<UserAssets>("info");
                return Content($"{info.User.Username} info fetched from session");
            }
            catch (System.NullReferenceException)
            {
                return Ok("Session expired login again. ");
            }
        }
    }
}
