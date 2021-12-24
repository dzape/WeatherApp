using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.DataContext;
using WeatherApp.Data.Entities;

namespace WeatherApp.Logic.Repository
{
    public class RepositoryAssets : IAssetsRepository<UserAssets>
    {
        DatabaseContext _context;
        public RepositoryAssets(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<UserAssets> CreateAssets(User _object)
        {
            var asset = new UserAssets();

            asset.Guid = new Guid();
            var obj = await _context.Assets.AddAsync(asset);
            _context.SaveChanges();
            return asset;
        }
    }
}
