using ImageRed.Application.Dto;
using ImageRed.Application.Interfaces;
using ImageRed.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageRed.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var album = await _albumService.GetAlbumAsync(id);
            return Ok(album);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> AddAlbum(AlbumDto albumDto)
        {
            var createdAlbum = await _albumService.AddAlbumAsync(albumDto);
            return CreatedAtAction(nameof(Get), new { id = createdAlbum.Id }, createdAlbum);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Update(int id, AlbumDto albumDto)
        {
            await _albumService.UpdateAlbumAsync(albumDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteAlbum(int id)
        {

            var albumDto = await _albumService.GetAlbumAsync(id);
            if (albumDto == null)
            {
                return NotFound();
            }
            await _albumService.DeleteAlbumAsync(albumDto);
            return NoContent();

        }
        [HttpPost("{albumId}/pictures/{pictureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> AddPictureToAlbum(int albumId, int pictureId)
        {
            await _albumService.AddPictureToAlbumAsync(albumId, pictureId);
            return NoContent();
        }

        [HttpDelete("{albumId}/pictures/{pictureId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> RemovePictureFromAlbum(int albumId, int pictureId)
        {
            await _albumService.RemovePictureFromAlbumAsync(albumId, pictureId);
            return NoContent();
        }
    }
}
