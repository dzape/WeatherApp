namespace WeatherApp.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        [Key]
        [Required]
        public Guid Guid { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


    }
}
