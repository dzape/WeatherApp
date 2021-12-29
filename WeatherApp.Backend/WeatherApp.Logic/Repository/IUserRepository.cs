using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Logic.IRepository
{
    public interface IUserRepository<T>
    {
        public T GetByEmail(string _objec);
        public T GetUser(T _object);
        public Task<T> Create(T _object);

        public void Update(T _object);

        public IEnumerable<T> GetAll();

        public T GetByGuid(Guid guid);

        public void Delete(T _object);

    }
}
