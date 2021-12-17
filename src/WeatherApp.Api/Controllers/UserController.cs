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

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="PutUser"></param>
        /// <returns>Ok</returns>
        [HttpPut]
        public async Task<IActionResult> PutUser(UserViewModel User)
        {
            if (_userRepository.UsernameMatch(User))
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            if (Authenticate(User))
            {
                await _context.SaveChangesAsync();
                return Ok();
            }

            return Unauthorized();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
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