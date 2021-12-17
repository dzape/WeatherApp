using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp.Api.Data.ViewModels
{
    public class UpdateUserViewModel
    {
        public string NewUsername { get; set; }
        public string OldUsername { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}
