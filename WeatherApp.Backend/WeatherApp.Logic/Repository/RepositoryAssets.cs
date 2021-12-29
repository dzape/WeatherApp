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

        public IEnumerable<UserAssets> GetAll()
        {
            return _context.Assets.ToList();
        }

        public async Task<UserAssets> CreateAssets(User _object)
        {
            var asset = new UserAssets();
            asset.Guid = Guid.NewGuid();
            asset.Role = Role.User;
            asset.User = _object;
            var obj = await _context.Assets.AddAsync(asset);
            _context.SaveChanges();
            return asset;
        }

        public void DeleteAssets(UserAssets _object)
        {
            _context.Remove(_object);
            _context.SaveChanges();
        }

        public void ActivateMail(UserAssets _object)
        {
            _object.Verified = true;
            _context.Assets.Update(_object);
            _context.SaveChanges();
        }
    }
}
