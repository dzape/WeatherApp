using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.DataContext;
using WeatherApp.Data.Entities;
using BC = BCrypt.Net.BCrypt;

namespace WeatherApp.Logic.Repository
{
    public class RepositoryAuth : IAuthRepository<User>
    {
        private readonly DatabaseContext _context;

        public RepositoryAuth(DatabaseContext context)
        {
            _context = context;
        }

        public bool IsAuthorized(User user)
        {
            var acc = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (user == null || !BC.Verify(user.Password, acc.Password))
            {
                return false;
            }
            return true;
        }
    }
}
