using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnyxScheduling.Models;
using System.Reflection.Emit;

namespace OnyxScheduling.Auth
{
    public class AuthDataContext : IdentityDbContext<User>
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
