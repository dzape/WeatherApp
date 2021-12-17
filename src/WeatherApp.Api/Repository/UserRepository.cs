using WeatherApp.Api.Data.Models;
using WeatherApp.Api.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Api.Data.ViewModels;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            return false;
        }

        public bool UsernameMatch(UserViewModel user)
        {
            try
            {
                var match = QueryUsersByName(user);
                if (match.Count() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public IEnumerable<User> QueryUsersByName(UserViewModel user)
        {
            var query = from q in _context.Users
                        where q.Username == user.Username
                        select q;

            return query;
        }

        public bool UsernameMatchOnUpdate(UpdateUserViewModel User)
        {
            var user = new UserViewModel();
            user.Username = User.NewUsername;

            try
            {
                var match = QueryUsersByName(user);
                if (match.Count() != 0)
                {
                    return true;
                }
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
}
