namespace WeatherApp.Api.Controllers
{
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data;
    using Microsoft.AspNetCore.Authorization;
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
    using WeatherApp.Api.Repository;
    using WeatherApp.Api.Data.ViewModels;

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IUserRepository _userRepository;
        public AuthController(UserDbContext context, IUserRepository userRepository)
        {
            this._context = context;
            this._userRepository = userRepository;
        }

        [HttpGet, Route("get")]
        public IEnumerable<string> Get()
            => new string[] { "Hello", "Name" };

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserViewModel account)
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

        [HttpPost, Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> CreateUser(UserViewModel User)
        {
            if (ModelState.IsValid)
            {
                if (!_userRepository.UsernameMatch(User))
                {
                    var user = new User();

                    user.Username = User.Username;
                    user.Email = User.Email;

                    user.Password = BC.HashPassword(User.Password);

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return StatusCode(200, "User created :)");
                }
                return StatusCode(201, "Username Exist.");
            }
            return StatusCode(201, "Model is not valid.");
        }


        private string GenerateToken(UserViewModel user)
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

        public bool Authenticate(UserViewModel account)
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