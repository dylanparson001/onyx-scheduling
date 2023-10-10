using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnyxScheduling.Auth
{
    public class AuthDataContext : IdentityDbContext<IdentityUser>
    {
        public AuthDataContext(DbContextOptions<AuthDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
