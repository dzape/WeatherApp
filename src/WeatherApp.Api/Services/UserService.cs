using WeatherApp.Api.Data.Models;
using WeatherApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp.Api.Services
{
    public interface IUserService
    {
        public User GetUserByUsername(string username);
        public bool DoesUserExist(string username);
        public IEnumerable<User> QueryUsersByName(string username);
    }

    public class UserService : IUserService
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
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
