namespace WeatherApp.Logic.Repository
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface ICityRepository<T> 
    {
        public Task<T> AddCity(T _object);
        public IEnumerable<T> GetCities();
        public void DeleteCity(T _object);
    }
}
