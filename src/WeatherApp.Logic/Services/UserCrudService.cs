using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Repository;
using BC = BCrypt.Net.BCrypt;

namespace WeatherApp.Logic.Services
{
    public class UserCrudService
    {
        private readonly AuthService _authService;

        private readonly IUserRepository<User> _userRepo;
        private readonly IAssetsRepository<UserAssets> _assetsRepo;

        public UserCrudService(IUserRepository<User> userRepo,
                                IAssetsRepository<UserAssets> assetsRepo,
                                AuthService authService)
        {
            _userRepo = userRepo;
            _assetsRepo = assetsRepo;
            _authService = authService;
        }

        // Create User
        public async Task<User> AddUser(User user)
        {
            if (!EmailMatch(user))
            {
                return null;
            }
            user.Password = BC.HashPassword(user.Password);
            return await _userRepo.Create(user);
        }

        public bool EmailMatch(User user)
        {
            try
            {
                var match = _userRepo.GetAll().Where(x => x.Email.Equals(user.Email)).ToList();
                if (match.Count == 0)
                {
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        // Update User
        public bool UpdateUser(User user)
        {
            if (_authService.Authenticate(user))
            {
                if (!EmailMatch(user))
                {
                    try
                    {
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
            return false;
        }

        // Delete User
        public bool DeleteUser(User user)
        {
            if (_authService.Authenticate(user))
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
