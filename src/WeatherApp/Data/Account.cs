using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Data;

namespace LoginForm.Data
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public List<Weather> WeatherList { get; set; }
    }
}
