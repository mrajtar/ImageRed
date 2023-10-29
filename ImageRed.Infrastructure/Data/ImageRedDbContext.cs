using ImageRed.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Infrastructure.Data
{
    public class ImageRedDbContext : DbContext
    {
        public ImageRedDbContext(DbContextOptions<ImageRedDbContext> options) : base(options)
        {
            
        }


        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
