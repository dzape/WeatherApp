using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Logic.Repository
{
    public interface IAuthRepository<T>
    {
        public bool IsAuthorized(T _object);
    }
}
