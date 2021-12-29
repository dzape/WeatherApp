using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;

namespace WeatherApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminController : ControllerBase
    {
        [HttpGet,Route("disableall")]
        [Authorize(Roles = "Admin")]
        public ActionResult DisableSessions()
        {
            HttpContext.Session.Clear();
            return Ok("Session is clear :)");
        }
    }
}
