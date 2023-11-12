using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using ImageRed.Application.Interfaces;
using ImageRed.Application.Dto;
using Microsoft.AspNetCore.Authorization;

[Route("api/pictures")]
[ApiController]

public class PictureController : ControllerBase
{
    private readonly IPictureService _pictureService;
    private readonly IFileService _fileService;
    private readonly IPictureRepository _pictureRepository;

    public PictureController(IPictureService pictureService, IFileService fileService, IPictureRepository pictureRepository)
    {
        _pictureService = pictureService;
        _fileService = fileService;
        _pictureRepository = pictureRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPictures()
    {
        var pictureDtos = await _pictureService.GetAllPicturesAsync();
        return Ok(pictureDtos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPicture(int id)
    {
        var pictureDto = await _pictureService.GetPictureAsync(id);
        if (pictureDto == null)
        {
            return NotFound();
        }
        return Ok(pictureDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> AddPicture([FromForm] PictureDto pictureDto)
    {
        if (pictureDto.ImageFile == null || pictureDto.ImageFile.Length == 0)
        {
            return BadRequest("No file is selected.");
        }


        var result = _fileService.SaveImage(pictureDto.ImageFile);
        if (result.Item1 == 0)
        {
            return BadRequest(result.Item2);
        }

 
        pictureDto.ImageUrl = result.Item2;

        var addedPicture = await _pictureService.AddPictureAsync(pictureDto);

        return CreatedAtAction(nameof(GetPicture), new { id = addedPicture.Id }, addedPicture);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> UpdatePicture(int id, [FromBody] PictureDto pictureDto)
    {
        if (id != pictureDto.Id)
        {
            return BadRequest("ID in the request body does not match the route.");
        }

        var existingPicture = await _pictureService.GetPictureAsync(id);
        if (existingPicture == null)
        {
            return NotFound();
        }

        await _pictureService.UpdatePictureAsync(id, pictureDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> DeletePicture(int id)
    {
        var existingPicture = await _pictureService.GetPictureAsync(id);
        if (existingPicture == null)
        {
            return NotFound();
        }


        if (_fileService.DeleteImage(existingPicture.ImageUrl))
        {
            await _pictureService.DeletePictureAsync(id);
            return NoContent();
        }

        return BadRequest("Failed to delete the image.");
    }
}
