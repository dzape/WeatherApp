namespace WeatherApp.Api.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        [HttpGet,Route("disableall")]
        public ActionResult DisableSessions()
        {
            HttpContext.Session.Clear();
            return Ok("Session is clear :)");
        }
    }
}
