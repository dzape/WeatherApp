using Microsoft.EntityFrameworkCore;
using WeatherApp.Api.Data.Models;

namespace WeatherApp.Api.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users {get; set;}
        public DbSet<City> Cities {get; set;}
    }
}