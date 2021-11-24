using Microsoft.EntityFrameworkCore;

namespace LoginForm.Data
{
    public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        { }

        public DbSet<Account> Accounts {get; set;}
    }
}
