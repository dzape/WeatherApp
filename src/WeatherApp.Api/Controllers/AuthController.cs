namespace WeatherApp.Api.Controllers
{
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data;
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
    using System.Net.Mail;
    using WeatherApp.Api.Services;
    using WeatherApp.Api.Helpers;

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        public AuthController(UserDbContext context, IUserRepository userRepository, IMailService mailService)
        {
            this._context = context;
            this._userRepository = userRepository;
            this._mailService = mailService;
        }

        [HttpGet, Route("get")]
        public IEnumerable<string> Get()
            => new string[] { "Hello", "Name" };

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] UserViewModel account)
        {
            //if ( account.emailActivated ) -> ...

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

                    var message = new MailRequest();
                    var link = "https://localhost:44322/api/activate/";
                    var userHash = BC.HashString(User.Username);

                    message.ToEmail = user.Email;
                    message.Subject = "Welcome to my Weather App, Activate Account.";
                    message.Body = "<h3> Hello </h3> <p> Hello, " + user.Username + " . You can activate your account on this <a href=" + link + userHash + ">link text</a>";

                    await _mailService.SendEmailAsync(message);

                    return Ok("Check your mail.");
                }
                return StatusCode(201, "Username Exist.");
            }
            return StatusCode(201, "Model is not valid.");
        }

        [HttpPut, Route("activate")]
        public async Task<ActionResult<User>> AcitvateUser(string userhash)
        {
            if(userhash != null)
            {
                // DECRYPT USERHASH AND
                // GETU USER 
                // UPDATE ACCOUNT VERFICATION TO TRUE 
                // REUTNR OK () ")
            }
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
                Expires = DateTime.UtcNow.AddHours(1),
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