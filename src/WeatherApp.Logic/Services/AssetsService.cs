using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;
using WeatherApp.Logic.Repository;

namespace WeatherApp.Logic.Services
{
    public class AssetsService
    {
        private readonly IAssetsRepository<UserAssets> _assetRepo;

        public AssetsService(IAssetsRepository<UserAssets> assetRepo)
        {
            _assetRepo = assetRepo;
        }

        // Create User
        public async Task<UserAssets> AddAssets(User user)
        {
            return await _assetRepo.CreateAssets(user);
        }
    }
}
