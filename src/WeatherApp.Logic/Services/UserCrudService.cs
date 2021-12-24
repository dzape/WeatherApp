using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;

namespace WeatherApp.Logic.Services
{
    public class UserCrudService
    {
        private readonly IUserRepository<User> _userRepo;
        public UserCrudService(IUserRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        // Create User
        public async Task<User> AddUser(User user)
        {
            return await _userRepo.Create(user);
        }

        // Update User
        public bool UpdateUser(User user)
        {
            try
            {
                // Other way ... Timcorey
                var DataList = _userRepo.GetAll().Where(x => x.Email.Equals(user.Email)).ToList();
                foreach (var item in DataList)
                {
                    item.Username = user.Username;
                    _userRepo.Update(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

        // Delete User
        public bool DeleteUser(string email)
        {
            try
            {
                var DataList = _userRepo.GetAll().Where(x => x.Email == email).ToList();
                foreach (var item in DataList)
                {
                    _userRepo.Delete(item);
                }
                return true;
            }
            catch (Exception)
            {
                return true;                
            }
        }
    }
}
