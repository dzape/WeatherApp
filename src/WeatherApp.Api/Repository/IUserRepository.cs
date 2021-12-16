using WeatherApp.Api.Data.Models;
using System.Collections.Generic;
using System.Collections;

namespace WeatherApp.Api.Repository
{
    public interface IUserRepository
    {
        public IEnumerable GetUserByUsername(string username);
        public bool DoesUserExist(string username);
        public IEnumerable QueryUsersByName(string username);
    }
}
