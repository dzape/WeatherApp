using LoginForm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginForm.Services
{
    public interface IAccountService
    {
        public bool UserDoesExist(string usernmae);
        //LOGIN

        //Register

    }

    public class AccountService : IAccountService
    {
        private readonly AccountDbContext _context;

        public bool UserDoesExist(string usernmae)
        {
            if(String.IsNullOrEmpty(usernmae))
            {
                return false;
            }
            
            var query = from r in _context.Accounts
                where r.Username.StartsWith(usernmae)
                orderby r.Username
                select r;
            
            if (query.First().Username == usernmae)
            {
                return true;
            }

            return false;
        }       
    }
}
