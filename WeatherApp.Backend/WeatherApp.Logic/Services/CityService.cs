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
    public class CityService
    {
        private readonly AuthService _authService;
        private readonly IUserRepository<User> _userRepo;
        private readonly IAssetsRepository<UserAssets> _assetsRepo;
        private readonly ICityRepository<City> _cityRepo;

        public CityService( IUserRepository<User> userRepo,
                            IAssetsRepository<UserAssets> assetsRepo,
                            ICityRepository<City> cityRepo,
                            AuthService authService)
        {
            _userRepo = userRepo;
            _assetsRepo = assetsRepo;
            _authService = authService;
            _cityRepo = cityRepo;
        }

        public IEnumerable<City> GetCitiesByUsername(string username)
        {
            var curentUser = _userRepo.GetByUsername(username);
            return _cityRepo.GetCities()
                            .Where(x => x.User.Id.Equals(curentUser.Id));
        }

        public async Task<City> AddCity(City city)
        {
            if (!CityMatch(city))
            {
                return null;
            }
            return await _cityRepo.AddCity(city);
        }

        public bool DeleteCity(City city)
        {
            try
            {
                _cityRepo.DeleteCity(city);
                return true;
            }
            catch (Exception)
            {
                return false;
            }    
        }

        public bool CityMatch(City city)
        {
            try
            {
                var match = _cityRepo.GetCities().Where(x => x.Name.Equals(city.Name)).ToList();
                if(match.Count == 0)
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
    }
}
