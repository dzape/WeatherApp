using System.Collections.Generic;
using System.Linq;
using WeatherApp.Api.Data;
using WeatherApp.Api.Data.Models;

namespace WeatherApp.Api.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _context;

        public CityRepository(UserDbContext context)
        {
            _context = context;
        }

        public bool DoesCityExist(string city_name, string username)
        {
            var cities = GetCitiesWithUserId(username);

            foreach (var i in cities)
            {
                if(i.Name == city_name)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<City> GetCitiesWithUserId(string username)
        {
            var query = from r in _context.Cities
                        where r.UserUsername.Equals(username)
                        orderby r.Id
                        select r;

            return query;
        }
    }
}
