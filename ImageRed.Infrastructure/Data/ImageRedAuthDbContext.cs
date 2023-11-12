using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageRed.Infrastructure.Data
{
    public class ImageRedAuthDbContext : IdentityDbContext
    {
        public ImageRedAuthDbContext(DbContextOptions<ImageRedAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var userRoleId = "da13d7fa-ca91-4207-9f91-6452408b0e4d";
            var adminRoleId = "38a3610d-5ba0-494e-bb2e-983c11fcc711";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
                };
            builder.Entity<IdentityRole>().HasData(roles);
        }


    }

}


