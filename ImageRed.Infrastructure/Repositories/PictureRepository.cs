using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using ImageRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly ImageRedDbContext _context;
        public PictureRepository(ImageRedDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Picture>> GetAllAsync()
        {
            return await _context.Pictures.ToListAsync();
        }
        public async Task<Picture> GetByIdAsync(int id)
        {
            return await _context.Pictures.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Picture> AddAsync(Picture picture)
        {
            await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();
            return picture;
        }

        public async Task UpdateAsync(Picture picture)
        {
            _context.Pictures.Update(picture);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Picture picture)
        {
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
        }
    }
}
