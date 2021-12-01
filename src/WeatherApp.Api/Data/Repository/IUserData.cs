namespace Weather.Api.Data.Repository
{
    using System.Collections.Generic;
    using WeatherApp.Api.Data.Models;
    public interface IUserData
    {
        public IEnumerable<User> GetUserByName(string name);
        User GetById(int id);
        User GetUsers();
        User Update(User updateUser);
        User Add(User newUser);
        User Delete(int id);
    }
}