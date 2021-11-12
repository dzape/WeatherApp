using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm.Services;

namespace LoginForm.Data.IRepository
{
    public interface IAccountData
    {
        public Account GetById(int id);
        public IEnumerable<Account> GetAccounts();
        public Account GetUserByName(string username);
    }

    public class InMemoryData : IAccountData
    {
        readonly List<Account> accounts;
        public InMemoryData()
        {
            accounts = new List<Account>
            {
                new Account { Id = 1, Username = "User1", Password = "Pass1"},
                new Account { Id = 2, Username = "User2", Password = "Pass2"},
                new Account { Id = 3, Username = "User3", Password = "Pass3"},
            };
        }
        public Account GetById(int id)
        {
            return accounts.SingleOrDefault(r => r.Id == id);
        }
        
        public IEnumerable<Account> GetAccounts()
        {
            return accounts;
        }

        public Account GetUserByName(string username)
        {
            throw new NotImplementedException();
        }

        //public Account GetUserByName(string username)
        //{


        //    return query.First();
        //}
    }
}
