using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Data.Entities
{
    public class UserAssets
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Role Role { get; set; }
        [Required]
        public Guid Guid { get; set; }
        public bool Verified { get; set; }
        public User User { get; set; }
    }
}
