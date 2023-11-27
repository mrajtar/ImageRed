using ImageRed.Application.Dto;
using ImageRed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetByIdAsync(int id);
        Task AddAsync(CommentDto commentDto);
        Task UpdateAsync(CommentDto commentDto);
        Task DeleteAsync(CommentDto commentDto);
    }
}
