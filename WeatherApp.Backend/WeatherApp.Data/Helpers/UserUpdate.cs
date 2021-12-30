using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data.Helpers
{
    public class UserUpdate
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
