using ImageRed.Application.Dto;
using ImageRed.Application.Interfaces;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace ImageRed.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return null;
            }
            var commentDto = MapCommentToCommentDto(comment);
            return commentDto;
        }

        public async Task AddAsync(CommentDto commentDto)
        {
            var comment = MapCommentDtoToComment(commentDto);
            comment.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateAsync(CommentDto commentDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var existingComment = await _commentRepository.GetByIdAsync(commentDto.CommentId);
            var userDetails = _httpContextAccessor.HttpContext.User;

            if (existingComment == null)
            {
                throw new KeyNotFoundException($"No comment found with this id");
            }

            if (existingComment.UserId != currentUserId && !userDetails.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to edit this comment.");
            }
            await _commentRepository.UpdateAsync(existingComment);
        }
        public async Task DeleteAsync(CommentDto commentDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var existingComment = await _commentRepository.GetByIdAsync(commentDto.CommentId);
            var userDetails = _httpContextAccessor.HttpContext.User;

            if (existingComment == null)
            {
                throw new KeyNotFoundException($"No comment found with this id");
            }

            if (existingComment.UserId != currentUserId && !userDetails.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this comment.");
            }

            await _commentRepository.DeleteAsync(existingComment);
        }

        private CommentDto MapCommentToCommentDto(Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.CommentId,
                PictureId = comment.PictureId,
                Text = comment.Text,
                Timestamp = comment.Timestamp
            };
        }
        private Comment MapCommentDtoToComment(CommentDto commentDto)
        {
            return new Comment
            {
                CommentId = commentDto.CommentId,
                PictureId = commentDto.PictureId,
                Text = commentDto.Text,
                Timestamp = commentDto.Timestamp
            };
        }
    }
}
