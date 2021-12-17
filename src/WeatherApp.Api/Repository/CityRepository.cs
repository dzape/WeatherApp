namespace WeatherApp.Api.Repository
{
    using System.Linq;
    using System.Collections.Generic;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Data.Models;

    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _context;

        public CityRepository(UserDbContext context)
        {
            _context = context;
        }

        public bool DoesCityExist(string city_name, string username)
        {
            var cities = GetCitiesWithUsername(username);

            foreach (var i in cities)
            {
                if(i.Name == city_name && i.UserUsername == username)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<City> GetCitiesWithUsername(string username)
        {
            var query = from r in _context.Cities
                        where r.UserUsername.Equals(username)
                        orderby r.Id
                        select r;

            return query;
        }
    }
}
