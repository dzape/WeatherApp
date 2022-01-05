namespace WeatherApp.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SessionMvc.App.Utilities;
    using System.Collections.Generic;
    using WeatherApp.Data.Entities;
    using WeatherApp.Logic.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AssetsService _assetsService;

        public AdminController(AssetsService assetService)
        {
            _assetsService = assetService;
        }

        [HttpGet, Route("test")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> Test()
            => new string[] { "Hello", "Perro" };

        [HttpGet, Route("get")]
        public ActionResult GetCookie()
        {
            var curentUser = new User();

            curentUser.Username = "Angular";

            var asset = _assetsService.GetAssets(curentUser);
            HttpContext.Session.Set<UserAssets>("info", asset);
            return Ok("Cookie set;");
        }

        [HttpGet,Route("disableall")]
        public ActionResult DisableSessions()
        {
            HttpContext.Session.Clear();
            return Ok("Session is clear :)");
        }
    }
}
