using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginForm.Data;
using BC = BCrypt.Net.BCrypt;

namespace LoginForm.Services
{
    public interface IAuthService
    {
        bool Authenticate(Account account);
    }

    public class AuthService : IAuthService
    {
        private readonly AccountDbContext _context;

        public AuthService(AccountDbContext context)
        {
            _context = context;
        }

        public bool Authenticate(Account account)
        {
            var acc = _context.Accounts.SingleOrDefault(x => x.Username == account.Username);

            if (account == null || !BC.Verify(account.Password, acc.PasswordHash))
            {
                return false;
            }
            return true;
        }
    }
}
