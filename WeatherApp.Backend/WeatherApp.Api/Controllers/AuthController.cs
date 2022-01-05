namespace WeatherApp.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SessionMvc.App.Utilities;
    using WeatherApp.Data.Entities;
    using WeatherApp.Data.Helpers;
    using WeatherApp.Logic.Services;
    using WeatherApp.Logic.Utilities;

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserCrudService _userCrudService;
        private readonly AssetsService _assetsService;
        private readonly AuthService _authService;
        private readonly IMailSender _mailSender;

        public AuthController( UserCrudService userService,
                               AssetsService assetsService,
                               AuthService authService,
                               IMailSender mailSender)
        {
            _userCrudService = userService;
            _assetsService = assetsService;
            _authService = authService;
            _mailSender = mailSender;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var curentUser = _userCrudService.GetUserByUsername(user.Username);
                if (_assetsService.IsMailVerified(curentUser))
                {
                    if (_authService.Authenticate(user))
                    {
                        var asset = _assetsService.GetAssets(curentUser);
                        HttpContext.Session.Set<UserAssets>("info", asset);
                        var token = _authService.GenerateToken(curentUser);

                        return Ok(new { Token = token });
                    }
                }
                return Ok("Mail not ver/");
            }
            return Ok("Check your inputs.");
        }

        [HttpPost, Route("register")]
        public async Task<Object> AddUser([FromBody] User user)
        {
            if (ModelState.IsValid)
            {
                if (_userCrudService.EmailMatch(user))
                {
                    await _userCrudService.AddUser(user);
                    await _assetsService.CreateAssets(user);

                    _mailSender.SendEmailAsync(user.Email);
                    return Ok("User was created");
                }
                return Ok("Email exist.");
            }
            return Ok("Check your input and try again !");
        }

        [HttpGet("{guid}"), Route("activation")]
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

                if(info.Role == Role.User)
                {
                    return Content("Hello user");
                }

                return Content($"{info.Role} info fetched from session");
            }
            catch (System.NullReferenceException)
            {
                return Ok("Session expired login again. ");
            }
        }
    }
}
