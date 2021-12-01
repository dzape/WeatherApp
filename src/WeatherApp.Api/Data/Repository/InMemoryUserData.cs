namespace Weather.Api.Data.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using WeatherApp.Api.Data.Models;
    public class InMemoryUserData : IUserData
    {
        readonly List<User> users;
        public InMemoryUserData()
        {
            users = new List<User>()
            {
                new User { Id = 1, Username = "Pero", Password = "Pero.123" },
                new User { Id = 1, Username = "Pero1", Password = "Pero.123" },
                new User { Id = 1, Username = "Pero2", Password = "Pero.123" }
            };
        }

        public User Add(User newRestaurant)
        {
            users.Add(newRestaurant);
            newRestaurant.Id = users.Max(r => r.Id) + 1;
            return newRestaurant;        
        }

        public User Delete(int id)
        {
            var user = users.FirstOrDefault(r => r.Id == id);
            if(user != null)
            {
                users.Remove(user);
            }
            return user;
        }

        public User GetById(int id)
        {
            return users.SingleOrDefault(r => r.Id == id);
        }
        public User GetUsers()
        {
            return users.SingleOrDefault();
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return from r in users
                   where string.IsNullOrEmpty(name) || r.Username.StartsWith(name)
                   orderby r.Username
                   select r;        
        }

        public User Update(User UpdatedUser)
        {
            var user = users.SingleOrDefault(r => r.Id == UpdatedUser.Id);
            if(user != null)
            {
                user.Username = UpdatedUser.Username;
            }
            return user;
        }
    }
}