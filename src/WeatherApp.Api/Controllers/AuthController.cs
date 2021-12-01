using WeatherApp.Api.Data.Models;
using WeatherApp.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using Weather.Api.Data.Repository;

namespace WeatherApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly UserDbContext _context;
        private readonly InMemoryUserData _inMemoryData;
        public AuthController(  InMemoryUserData inMemoryData)
        {
            //this._context = context;
            this._inMemoryData = inMemoryData;
        }

        [HttpGet, Route("get")]
        public IEnumerable<string> Get()
            => new string[] { "Hello", "Name" };

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]User account)
        {
            if (account == null)
                return BadRequest("Invalid client request");

            if (Authenticate(account)) 
            {
                var secKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signingCridentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44316/",
                    audience: "http://localhost:44316",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCridentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }

            return Unauthorized();
        }

        public bool Authenticate(User account)
        {

            var acc = _inMemoryData.GetUserByName(account.Username).SingleOrDefault(x => x.Username == account.Username);

            if (account == null || !BC.Verify(account.Password, acc.Password))
            {
                return false;
            }
            return true;
        }
    }
}