using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Api.Data;
using WeatherApp.Api.Data.Models;

namespace WeatherApp.Api.Services
{
    public interface ICityService
    {
        public IEnumerable<City> GetCitiesWithUserId(int user_id);
        public bool DoesCityExist(string city_name, int user_id);
    }

    public class CityService : ICityService
    {
        private readonly UserDbContext _context;

        public CityService(UserDbContext context)
        {
            _context = context;
        }

        public bool DoesCityExist(string city_name, int user_id)
        {
            var cities = GetCitiesWithUserId(user_id);

            foreach (var i in cities)
            {
                if(i.Name == city_name)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<City> GetCitiesWithUserId(int user_id)
        {
            var query = from r in _context.Cities
                        where r.UserId.Equals(user_id)
                        orderby r.Id
                        select r;

            return query;
        }
    }
}
