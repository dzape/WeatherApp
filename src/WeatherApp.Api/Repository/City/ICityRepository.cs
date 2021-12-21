namespace WeatherApp.Api.Repository
{
    using System.Collections;
    using WeatherApp.Api.Data.Models;
    using WeatherApp.Api.Data.ViewModels;

    public interface ICityRepository
    {
        public IEnumerable GetCitiesWithUsername(string username);
        public City GetCity(CityViewModel city);
        public bool CityMatch(CityViewModel city);
        public IEnumerable GetCities(User user);
    }
}
