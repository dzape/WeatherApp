using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.IRepository;
using WeatherApp.Logic.Repository;

namespace WeatherApp.Logic.Services
{
    public class AssetsService
    {
        private readonly IAssetsRepository<UserAssets> _assetRepo;
        private readonly IUserRepository<User> _userRepository;

        public AssetsService(IAssetsRepository<UserAssets> assetRepo, IUserRepository<User> userRepository)
        {
            _assetRepo = assetRepo;
            _userRepository = userRepository;
        }

        // Get Assets for user
        public UserAssets GetAssets(User user)
        {
            var curentUser = _userRepository.GetAll().Where(x => x.Username.Equals(user.Username)).First();
            return _assetRepo.GetAll().Where(x => x.User.Id.Equals(curentUser.Id)).First();
        }

        // onActivate User
        public void ActivateMail(Guid guid)
        {
            var curentAsset = _assetRepo.GetAll().Where(x => x.Guid.Equals(guid)).FirstOrDefault();
            if(curentAsset.Verified == false)
            {
                _assetRepo.ActivateMail(curentAsset);
            }
        }

        // isMail Ver
        public bool IsMailVerified(User user)
        {
            var curentUser = _userRepository.GetAll().Where(x => x.Username.Equals(user.Username)).First();
            var curentAsset = _assetRepo.GetAll().Where(x => x.User.Id.Equals(curentUser.Id)).FirstOrDefault();

            if(curentAsset.Verified)
                return true;

            return false;
        }

        // onCreate User
        public async Task<UserAssets> CreateAssets(User user)
        {
            return await _assetRepo.CreateAssets(user);
        }

        public bool DeleteAssets(UserAssets userAssets)
        {
            try
            {
                var DataList = _assetRepo.GetAll().Where(x => x.User.Id.Equals(userAssets.User.Id));
                foreach (var item in DataList)
                {
                    _assetRepo.DeleteAssets(userAssets);
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
