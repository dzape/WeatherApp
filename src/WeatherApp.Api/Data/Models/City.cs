namespace WeatherApp.Api.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string UserUsername { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}