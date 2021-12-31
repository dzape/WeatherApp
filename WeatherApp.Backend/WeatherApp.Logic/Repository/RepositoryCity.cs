using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.DataContext;
using WeatherApp.Data.Entities;

namespace WeatherApp.Logic.Repository
{
    public class RepositoryCity : ICityRepository<City>
    {
        DatabaseContext _context;
        public RepositoryCity(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<City> AddCity(City _object)
        {
            var obj = await _context.Cities.AddAsync(_object);
            _context.SaveChanges();
            return obj.Entity;
        }

        public void DeleteCity(City _object)
        {
            _context.Remove(_object);
            _context.SaveChanges();
        }

        public IEnumerable<City> GetCities()
        {
            try
            {
                return _context.Cities.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
