using Microsoft.Extensions.Configuration;
using System.IO;

namespace WeatherApp.Data.DataContext
{
    public class AppConfiguration
    {
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory() , "../WeatherApp.Api/appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSettings = root.GetSection("ConnectionStrings:DefaultConnection");
            SqlConnectionString = appSettings.Value;
        }

        public string SqlConnectionString { get; set; }
    }
}
