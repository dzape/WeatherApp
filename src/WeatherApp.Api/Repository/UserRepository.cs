using WeatherApp.Api.Data.Models;
using WeatherApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Api.Data.ViewModels;
using System.Collections;

namespace WeatherApp.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string username)
        {
            try
            {
                var match = GetUserByUsername(username);
                if (match != null)
                    return true;
            }
            catch (InvalidOperationException)
            {
                return false;
                throw new InvalidOperationException("User exist");
            }
            return false;
        }

        public IEnumerable GetUserByUsername(string username)
        {
            var query = QueryUsersByName(username);
            return query;
        }

        public IEnumerable QueryUsersByName(string username)
        {
            var query = _context.Users
                .Where(x => x.Username == username|| x.Username != null)
                .Select(x => new { x.Email, x.Username, x.Password});                                                                    

            return query;
        }
    }
}
