using WeatherApp.Api.Data.Models;
using WeatherApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var match = GetUserByUsername(username).Username.ToString();
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

        public User GetUserByUsername(string username)
        {
            var query = QueryUsersByName(username).First();
            return query;
        }

        public IEnumerable<User> QueryUsersByName(string username)
        {
            var query = from r in _context.Users
                        where r.Username.StartsWith(username) || string.IsNullOrEmpty(username)
                        orderby r.Username
                        select r;

            return query;
        }
    }
}
