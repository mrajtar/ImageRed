using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Infrastructure.Data
{
    public class ImageRedAuthDbContext : IdentityDbContext
    {
        public ImageRedAuthDbContext(DbContextOptions<ImageRedAuthDbContext> options) : base(options) 
        {
            
        }

    }
}
