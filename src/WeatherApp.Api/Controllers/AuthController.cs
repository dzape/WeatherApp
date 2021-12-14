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
using WeatherApp.Api.Services;

namespace WeatherApp.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IUserService _userService;
        public AuthController(UserDbContext context, IUserService userService)
        {
            this._context = context;
            this._userService = userService;
        }

        [HttpGet, Route("get")]
        public IEnumerable<string> Get()
            => new string[] { "Hello", "Name" };

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User account)
        {
            if (account == null)
                return BadRequest("Invalid client request");

            if (Authenticate(account))
            {

                var token = GenerateToken(account);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        // POST: api/Users
        [HttpPost, Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            if (!_userService.DoesUserExist(User.Username))
            {
                User.Password = BC.HashPassword(User.Password);
                _context.Users.Add(User);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = User.Id }, User);
            }
            return StatusCode(201);
        }


        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("superSecretKey@345");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public bool Authenticate(User account)
        {

            var acc = _context.Users.SingleOrDefault(x => x.Username == account.Username);

            if (account == null || !BC.Verify(account.Password, acc.Password))
            {
                return false;
            }
            return true;
        }
    }
}