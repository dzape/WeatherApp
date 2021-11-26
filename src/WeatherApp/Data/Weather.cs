using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Data
{
    public class Weather
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int AccountId { get; set; }
    }
}
