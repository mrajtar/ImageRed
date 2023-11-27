using ImageRed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(Comment comment);
    }
}
