using ImageRed.Application.Interfaces;
using ImageRed.Application.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ImageRed.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CommentDto>> GetComment(int id)
        {
            var commentDto = await _commentService.GetByIdAsync(id);

            if (commentDto == null)
            {
                return NotFound();
            }

            return commentDto;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<CommentDto>> AddComment(CommentDto commentDto)
        {
            await _commentService.AddAsync(commentDto);

            return CreatedAtAction(nameof(GetComment), new { id = commentDto.PictureId }, commentDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, CommentDto commentDto)
        {
            if (id != commentDto.PictureId)
            {
                return BadRequest();
            }

            await _commentService.UpdateAsync(commentDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var commentDto = await _commentService.GetByIdAsync(id);

            if (commentDto == null)
            {
                return NotFound();
            }

            await _commentService.DeleteAsync(commentDto);

            return NoContent();
        }
    }
}
