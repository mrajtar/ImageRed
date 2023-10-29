using ImageRed.Application.Dto;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Services
{
    public class PictureService
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

        public async Task<PictureDto> AddPictureAsync(PictureDto pictureDto)
        {
            var picture = MapPictureDtoToPicture(pictureDto);
            var addedPicture = await _pictureRepository.AddAsync(picture);
            return MapPictureToPictureDto(addedPicture);
        }

        public async Task UpdatePictureAsync(PictureDto pictureDto)
        {
            var picture = MapPictureDtoToPicture(pictureDto);
            await _pictureRepository.UpdateAsync(picture);
        }

        public async Task DeletePictureAsync(int id)
        {
            var picture = await _pictureRepository.GetByIdAsync(id);
            if (picture != null)
            {
                await _pictureRepository.DeleteAsync(picture);
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
