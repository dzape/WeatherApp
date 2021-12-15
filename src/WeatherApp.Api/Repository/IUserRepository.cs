using WeatherApp.Api.Data.Models;
using System.Collections.Generic;

namespace WeatherApp.Api.Repository
{
    public interface IUserRepository
    {
        public User GetUserByUsername(string username);
        public bool DoesUserExist(string username);
        public IEnumerable<User> QueryUsersByName(string username);
    }
}
