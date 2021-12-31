using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;

namespace WeatherApp.Logic.Repository
{
    public interface ICityRepository<T> 
    {
        public Task<T> AddCity(T _object);
        public IEnumerable<City> GetCities();
        public void DeleteCity(T _object);
    }
}
