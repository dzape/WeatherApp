using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Data.Entities;

namespace WeatherApp.Logic.Repository
{
    public interface IAssetsRepository<T>
    {
        public Task<T> CreateAssets(User _object);
    }
}
