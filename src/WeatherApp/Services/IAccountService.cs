using LoginForm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services
{
    public interface IAccountService
    {
        public Account GetUserByUsername(string username);

        public bool DoesUserExist(string username);
        public IEnumerable<Account> QueryAccountsByName(string username);
    }

    public class AccountService : IAccountService
    {
        private readonly AccountDbContext _context;

        public AccountService(AccountDbContext context)
        {
            _context = context;
        }

        public bool DoesUserExist(string username)
        {
            try
            {
                var match = GetUserByUsername(username).Username.ToString();
                if (match != null)
                    return true;
            }
            catch (InvalidOperationException)
            {
                return false;
                throw new InvalidOperationException("User exist");
            }
            return false;
        }

        public Account GetUserByUsername(string username)
        {
            var query = QueryAccountsByName(username).First();
            return query;
        }

        public IEnumerable<Account> QueryAccountsByName(string username)
        {
            var query = from r in _context.Accounts
                        where r.Username.StartsWith(username) || string.IsNullOrEmpty(username)
                        orderby r.Username
                        select r;

            return query;
        }
    }
}
