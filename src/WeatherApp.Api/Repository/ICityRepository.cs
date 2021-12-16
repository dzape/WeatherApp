using System.Collections.Generic;
using WeatherApp.Api.Data.Models;

namespace WeatherApp.Api.Repository
{
    public interface ICityRepository
    {
        public IEnumerable<City> GetCitiesWithUsername(string username);
        public bool DoesCityExist(string city_name, string username);
    }
}
