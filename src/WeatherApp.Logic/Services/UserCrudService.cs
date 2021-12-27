using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Repository;
using BC = BCrypt.Net.BCrypt;

namespace WeatherApp.Logic.Services
{
    public class UserCrudService
    {
        private readonly IUserRepository<User> _userRepo;
        private readonly IAssetsRepository<UserAssets> _assetsRepo;
        private readonly IAuthRepository<User> _authRepo;

        public UserCrudService( IUserRepository<User> userRepo, 
                                IAssetsRepository<UserAssets> assetsRepo,
                                IAuthRepository<User> authRepo)
        {
            _userRepo = userRepo;
            _assetsRepo = assetsRepo;
            _authRepo = authRepo;
        }

        // Create User
        public async Task<User> AddUser(User user)
        {
            // If username or email match return false ?
            user.Password = BC.HashPassword(user.Password);
            return await _userRepo.Create(user);
        }

        // Update User
        public bool UpdateUser(User user)
        {
            if (_authRepo.IsAuthorized(user))
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
                    return false;
                }
            }
            return false;
        }

        // Delete User
        public bool DeleteUser(User user)
        {
            if (_authRepo.IsAuthorized(user))
            {
                try
                {
                    var userData = _userRepo.GetAll().Where(x => x.Email == user.Email).ToList().First();
                    var userAssets = _assetsRepo.GetAll().Where(x => x.User.Id.Equals(userData.Id)).First();
                    _userRepo.Delete(userData);
                    _assetsRepo.DeleteAssets(userAssets);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
