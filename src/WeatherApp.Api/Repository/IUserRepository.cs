namespace WeatherApp.Api.Repository
{
    using System.Collections.Generic;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;
    public interface IUserRepository
    {
        public User GetUser(string username);
        public bool UsernameMatch(UserViewModel user);
        public bool UsernameMatchOnUpdate(UpdateUserViewModel user);

        public IEnumerable<User> QueryUsersByName(UserViewModel user);

    }
}
