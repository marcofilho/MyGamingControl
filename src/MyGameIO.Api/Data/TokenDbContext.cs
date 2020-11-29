using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyGameIO.App.Data
{
    public class TokenDbContext : IdentityDbContext
    {
        public TokenDbContext(DbContextOptions<TokenDbContext> options)
            : base(options)
        {
        }
    }
}
