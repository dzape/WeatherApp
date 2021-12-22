namespace WeatherApp.Api.Repository
{
    using System.Linq;
    using WeatherApp.Api.Data;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;
    using System.Collections;

    public class CityRepository : ICityRepository
    {
        private readonly UserDbContext _context;
        private readonly IUserRepository _userRepository;

        public CityRepository(UserDbContext context, IUserRepository userRepository)
        {
            _context = context;
            this._userRepository = userRepository;
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
            var user = _userRepository.GetUser(city.UserUsername);

            var query = from i in _context.Cities
                           where i.Name.Equals(city.Name) && i.UserId.Equals(user.Id)
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

        public IEnumerable GetCitiesById(int id)
        {
            var query = from r in _context.Cities
                        where r.Id.Equals(id)
                        orderby r.Id
                        select r;

            return query;
        }
    }
}
