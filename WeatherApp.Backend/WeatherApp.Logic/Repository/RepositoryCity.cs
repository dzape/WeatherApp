namespace WeatherApp.Logic.Repository
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using WeatherApp.Data.DataContext;
    using WeatherApp.Data.Entities;
    public class RepositoryCity : ICityRepository<City>
    {
        DatabaseContext _context;
        public RepositoryCity(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<City> AddCity(City _object)
        {
            await _context.Cities.AddAsync(_object);
            _context.SaveChanges();
            return _object;
        }

        public void DeleteCity(City _object)
        {
            _context.Remove(_object);
            _context.SaveChanges();
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.ToList();
        }
    }
}
