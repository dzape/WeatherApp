namespace WeatherApp.Api.Data.Models
{
    using System.Collections.Generic;
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<City> CityList { get; set; }
    }
}