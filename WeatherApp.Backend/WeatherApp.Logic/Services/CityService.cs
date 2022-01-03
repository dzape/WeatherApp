using System;
using System.Collections;
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

        public City GetCity(City city)
        {
            return _cityRepo.GetCities().Where(x => x.User != null).First();
        }

        public IEnumerable GetCitiesByUsername(User user)
        {
            return _cityRepo.GetCities().Where(x => x.User != null).Select(x => x.Name);
        }

        public async Task<City> AddCity(City city)
        {
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
                var cities = GetCitiesByUsername(city.User);
                foreach (var item in cities)
                {
                    if(city.Name.ToString() == item.ToString())
                    {
                        return true;
                        break;
                    }
                }
            }
            catch (System.Exception)
            {

                return false;

            }
            return false;
        }
    }
}
