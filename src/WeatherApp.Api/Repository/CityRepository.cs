namespace WeatherApp.Api.Repository
{
    using System.Linq;
    using System.Collections.Generic;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;

    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _context;

        public CityRepository(UserDbContext context)
        {
            _context = context;
        }

        public City GetCity(CityViewModel city)
        {
            var query = from i in _context.Cities
                           where i.Name.Equals(city.Name) && i.UserUsername.Equals(city.UserUsername)
                           select i;

            return query.First();
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
