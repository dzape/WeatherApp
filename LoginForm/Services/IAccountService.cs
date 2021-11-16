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
        public bool DoesUsernameExist(string username);
    }

    public class AccountService : IAccountService
    {
        private readonly AccountDbContext _context;

        public AccountService(AccountDbContext context)
        {
            _context = context;
        }

        public bool DoesUsernameExist(string username)
        {
            if (!String.IsNullOrEmpty(username))
            {
                if (username.Equals(GetUserByUsername(username).Username))
                {
                    return true;
                }
            }
            return false;
        }

        public Account GetUserByUsername(string username)
        {
            var query = from r in _context.Accounts
                        where r.Username.StartsWith(username) || string.IsNullOrEmpty(username)
                        orderby r.Username
                        select r;

            if (!string.IsNullOrEmpty(query.First().Username))
            {
                return query.First();
            }
            else
                return null;
        }
    }
}
