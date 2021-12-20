namespace WeatherApp.Api.Repository
{
    using System.Collections.Generic;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;

    public interface ICityRepository
    {
        public IEnumerable<City> GetCitiesWithUsername(string username);
        public City GetCity(CityViewModel city);

        public bool DoesCityExist(string city_name, string username);
    }
}
