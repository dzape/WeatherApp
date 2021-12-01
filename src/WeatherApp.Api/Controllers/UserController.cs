namespace Weather.Api.Controllers
{
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BC = BCrypt.Net.BCrypt;
    using Weather.Api.Data.Repository;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly InMemoryUserData _context;
        private readonly UserService _userservice;

        public UsersController(InMemoryUserData context, UserService userService)
        {
            this._context = context;
            this._userservice = userService;
        }

        // GET: api/Users
        [HttpGet]
        public User GetUsers()
        {
            return _context.GetUsers();
        }

        // GET: api/Users/58
        // [HttpGet("{id:int}")]
        // public async Task<ActionResult<User>> GetUser(int id)
        // {
        //     var User = await _context.Users.FindAsync(id);

        //     if (User == null)
        //     {
        //         return NotFound();
        //     }

        //     return User;
        // }

        // [HttpGet("{username}")]
        // public async Task<ActionResult<User>> GetByUsername(string username)
        // {
        //     if (_userservice.DoesUserExist(username))
        //     {
        //         var User = _userservice.GetUserByUsername(username);
        //         return User;
        //     }
        //     return NotFound();
        // }

        // [HttpGet]
        // [Route("getid/")]
        // public int GetIdByUsername(string username)
        // {
        //    if (_userservice.DoesUserExist(username))
        //    {
        //        var User = _userservice.GetUserByUsername(username);
        //        return User.Id;
        //    }
        //    return 0;
        // }

        // // PUT: api/Users/5
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutUser(int id, User User)
        // {
        //     if (id != User.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(User).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!UserExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Users
        // [HttpPost]
        // public async Task<ActionResult<User>> PostUser(User User)
        // {
        //     if (!_userservice.DoesUserExist(User.Username))
        //     {
        //         User.Password = BC.HashPassword(User.Password);
        //         _context.Users.Add(User);
        //         await _context.SaveChangesAsync();

        //         return CreatedAtAction("GetUser", new { id = User.Id }, User);
        //     }
        //     return StatusCode(201);
        // }

        // // DELETE: api/Users/5
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteUser(int id)
        // {
        //     var User = await _context.Users.FindAsync(id);
        //     if (User == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Users.Remove(User);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        // private bool UserExists(int id)
        // {
        //     return _context.Users.Any(e => e.Id == id);
        // }
    }
}