namespace WeatherApp.Api.Repository
{
    using System.Linq;
    using System.Collections.Generic;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;
    using System.Collections;

    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _context;

        public CityRepository(UserDbContext context)
        {
            _context = context;
        }

        /* Returns list of all cities taht one user have saved*/
        public IEnumerable GetCities(User user)
        {
            var query = from i in _context.Cities
                        where i.UserId.Equals(user.Id)
                        select i.Name;

            return query;
        }

        /* Retyrns Specific city. (Delete) */
        public City GetCity(CityViewModel city)
        {
            var query = from i in _context.Cities
                           where i.Name.Equals(city.Name) && i.UserUsername.Equals(city.UserUsername)
                           select i;

            return query.First();
        }

        /* Check if city exist and returns bool value. */
        public bool CityMatch(CityViewModel city)
        {
            if(city != null)
            {
                try
                {
                    var match = GetCity(city);
                    if(match != null)
                    {
                        return true;
                    }
                }
                catch (System.Exception)
                {

                    return false;
                }
            }
            return true;
        }

        public IEnumerable GetCitiesWithUsername(string username)
        {
            var query = from r in _context.Cities
                        where r.UserUsername.Equals(username)
                        orderby r.Id
                        select r;

            return query;
        }
    }
}
