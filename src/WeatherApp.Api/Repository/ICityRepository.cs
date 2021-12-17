namespace WeatherApp.Api.Repository
{
    using System.Collections.Generic;
    using WeatherApp.Api.Data.Models;
    public interface ICityRepository
    {
        public IEnumerable<City> GetCitiesWithUsername(string username);
        public bool DoesCityExist(string city_name, string username);
    }
}
