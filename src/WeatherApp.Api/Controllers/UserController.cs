namespace Weather.Api.Controllers
{
    using WeatherApp.Api.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using BC = BCrypt.Net.BCrypt;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Repository;
    using WeatherApp.Api.Data.ViewModels;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IUserRepository _userRepository;

        public UsersController(UserDbContext context, IUserRepository userRepository)
        {
            this._context = context;
            this._userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UpdateUserViewModel User)
        {
            if (Authenticate(User))
            {
                if (_userRepository.UsernameMatchOnUpdate(User))
                {
                    return BadRequest();
                }

                var acc = _context.Users.SingleOrDefault(x => x.Username == User.OldUsername);
                acc.Username = User.NewUsername;
                _context.Entry(acc).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }


            }
            return Unauthorized();
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var userObj = _userRepository.GetUser(username);

            _context.Users.Remove(userObj);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public bool Authenticate(UpdateUserViewModel account)
        {
            var acc = _userRepository.GetUser(account.OldUsername);

            if (account == null || !BC.Verify(account.Password, acc.Password)) ;
            {
                return false;
            }
            return true;
        }
    }
}