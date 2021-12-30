using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data.DataContext;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;

namespace WeatherApp.Logic.Repository
{
    public class RepositoryUser : IUserRepository<User>
    {
        DatabaseContext _context;
        public RepositoryUser(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User _object)
        {
            var obj = await _context.Users.AddAsync(_object);
            _context.SaveChanges();
            return obj.Entity;
        }
        
        public void Delete(User _object)
        {
            _context.Remove(_object);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetByUsername(string username)
        {
            return (User)_context.Users.Where(x => x.Username.Equals(username)).First();
        }
        
        public User GetByEmail(string email)
        {
            return (User)_context.Users.Where(x => x.Email.Equals(email)).First();
        }

        public User GetByGuid(Guid guid)
        {
            var query = _context.Assets.Where(x => x.Guid.Equals(guid)).FirstOrDefault();
            return (User)_context.Users.Where(x => x.Id.Equals(query.Id));
        }

        public User GetUser(User _object)
        {
            throw new NotImplementedException();
        }

        public void Update(User _object)
        {
            _context.Users.Update(_object);
            _context.SaveChanges();
        }
    }
}
