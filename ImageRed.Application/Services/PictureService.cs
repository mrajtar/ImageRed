using ImageRed.Application.Dto;
using ImageRed.Application.Interfaces;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureService(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<IEnumerable<PictureDto>> GetAllPicturesAsync()
        {
            var pictures = await _pictureRepository.GetAllAsync();
            var pictureDtos = new List<PictureDto>();

            foreach (var picture in pictures)
            {
                var pictureDto = MapPictureToPictureDto(picture);
                pictureDtos.Add(pictureDto);
            }

            return pictureDtos;
        }

        public async Task<PictureDto> GetPictureAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            if (picture == null)
            {
                return null;
            }

            var pictureDto = MapPictureToPictureDto(picture);
            return pictureDto;
        }

        public async Task<PictureDto> AddPictureAsync(PictureDto pictureDto, string UserId)
        {

            var picture = MapPictureDtoToPicture(pictureDto);
            var addedPicture = await _pictureRepository.AddAsync(picture, UserId);
            return MapPictureToPictureDto(addedPicture);
        }

        public async Task UpdatePictureAsync(int id, PictureDto pictureDto, string UserId)
        {
            var existingPicture = await _pictureRepository.GetByIdAsync(id);

            if (existingPicture == null)
            {
                return;
            }


            if (UserId == existingPicture.UserId)
            {
                var picture = MapPictureDtoToPicture(pictureDto);
                await _pictureRepository.UpdateAsync(picture, UserId);
            }
            else
            {
                throw new InvalidOperationException("User does not have permission to update the picture.");
            }
        }

        public async Task DeletePictureAsync(int id, string UserId)
        {
            var existingPicture = await _pictureRepository.GetByIdAsync(id);

            if (existingPicture == null)
            {
                throw new InvalidOperationException("Picture not found");
            }

            if (UserId == existingPicture.UserId)
            {
                await _pictureRepository.DeleteAsync(existingPicture, UserId);
            }
            else
            {
                throw new InvalidOperationException("User does not have permission to delete the picture.");
            }
        }

        private PictureDto MapPictureToPictureDto(Picture picture)
        {
            return new PictureDto
            {
                Id = picture.Id,
                Title = picture.Title,
                Description = picture.Description,
                ImageUrl = picture.ImageUrl,
                Tag = picture.Tag,
            };
        }

        private Picture MapPictureDtoToPicture(PictureDto pictureDto)
        {
            return new Picture
            {
                Id = pictureDto.Id,
                Title = pictureDto.Title,
                Description = pictureDto.Description,
                ImageUrl = pictureDto.ImageUrl,
                Tag = pictureDto.Tag,
            };
        }
    }
}
