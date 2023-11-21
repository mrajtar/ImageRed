using ImageRed.Application.Dto;
using ImageRed.Application.Interfaces;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImageRed.Application.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PictureService(IPictureRepository pictureRepository, IHttpContextAccessor httpContextAccessor)
        {
            _pictureRepository = pictureRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<PictureDto>> GetAllPicturesAsync()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value;
            // get details about the user to check for his role
            var userDetails = _httpContextAccessor.HttpContext.User;
            var pictures = await _pictureRepository.GetAllAsync();
            var pictureDtos = new List<PictureDto>();

            foreach (var picture in pictures)
            {
                // if picture is set to private, user is not an admin and not the one who uploaded it, the picture is skipped
                if (picture.isPrivate && !userDetails.IsInRole("Admin") && picture.UserId != currentUserId)
                {
                    continue;
                }
                var pictureDto = MapPictureToPictureDto(picture);
                pictureDtos.Add(pictureDto);
            }

            return pictureDtos;
        }

        public async Task<PictureDto> GetPictureAsync(int id)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var userDetails = _httpContextAccessor.HttpContext.User;
            var picture = await _pictureRepository.GetByIdAsync(id);
            if (picture == null)
            {
                return null;
            }
            if (picture.isPrivate && !userDetails.IsInRole("Admin") && picture.UserId != currentUserId)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            var pictureDto = MapPictureToPictureDto(picture);
            return pictureDto;
        }

        public async Task<PictureDto> AddPictureAsync(PictureDto pictureDto, string UserId)
        {

            var picture = MapPictureDtoToPicture(pictureDto);
            picture.UserId = UserId;
            var addedPicture = await _pictureRepository.AddAsync(picture, UserId);
            return MapPictureToPictureDto(addedPicture);
        }

        public async Task UpdatePictureAsync(int id, PictureDto pictureDto, string UserId)
        {
            var existingPicture = await _pictureRepository.GetByIdAsync(id);
            var userDetails = _httpContextAccessor.HttpContext.User;
            if (existingPicture == null)
            {
                return;
            }

            if (UserId == existingPicture.UserId || userDetails.IsInRole("Admin"))
            {
                var picture = MapOldWithNew(pictureDto, existingPicture);
                await _pictureRepository.UpdateAsync(picture, UserId);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }


        public async Task DeletePictureAsync(int id, string UserId)
        {
            var existingPicture = await _pictureRepository.GetByIdAsync(id);
            var userDetails = _httpContextAccessor.HttpContext.User;

            if (existingPicture == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (UserId == existingPicture.UserId || userDetails.IsInRole("Admin"))
            {
                await _pictureRepository.DeleteAsync(existingPicture, UserId);
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
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
                Author = picture.Author,
                Tag = picture.Tag,
                isPrivate = picture.isPrivate,
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
                Author = pictureDto.Author,
                Tag = pictureDto.Tag,
                isPrivate = pictureDto.isPrivate,
            };
        }
        private Picture MapOldWithNew(PictureDto pictureDto, Picture existingPic)
        {
            if (pictureDto.Title != null)
            {
                existingPic.Title = pictureDto.Title;
            }


            if (pictureDto.Description != null)
            {
                existingPic.Description = pictureDto.Description;
            }

            if (pictureDto.Tag != null)
            {
                existingPic.Tag = pictureDto.Tag;
            }

            if (pictureDto.Author != null)
            {
                existingPic.Author = pictureDto.Author;
            }

            if (pictureDto.isPrivate != existingPic.isPrivate)
            {   
                existingPic.isPrivate = pictureDto.isPrivate;
            }
            return existingPic;
        }
    }
}
