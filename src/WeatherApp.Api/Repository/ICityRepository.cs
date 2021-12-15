using System.Collections.Generic;
using WeatherApp.Api.Data.Models;

namespace WeatherApp.Api.Repository
{
    public interface ICityRepository
    {
        public IEnumerable<City> GetCitiesWithUserId(string username);
        public bool DoesCityExist(string city_name, string username);
    }
}
