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
    public class CommentRepository : ICommentRepository
    {
        private readonly ImageRedDbContext _context;

        public CommentRepository(ImageRedDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.SingleOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

    }
}
