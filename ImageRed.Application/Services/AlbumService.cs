using ImageRed.Application.Dto;
using ImageRed.Application.Interfaces;
using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlbumService(IAlbumRepository albumRepository, IHttpContextAccessor httpContextAccessor)
        {
            _albumRepository = albumRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AlbumDto> GetAlbumAsync(int id)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value;
            var userDetails = _httpContextAccessor.HttpContext.User;
            var album = await _albumRepository.GetByIdAsync(id);
            if (album == null)
            {
                return null;
            }
            if (album.isPrivate && !userDetails.IsInRole("Admin") && album.UserId != currentUserId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this album.");
            }
            var albumDto = MapAlbumToAlbumDto(album);
            return albumDto;
        }
        public async Task<AlbumDto> AddAlbumAsync(AlbumDto albumDto)
        {
            var album = MapAlbumDtoToAlbum(albumDto);
            album.UserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            album.Pictures = new List<Picture>();
            var addedAlbum = await _albumRepository.AddAsync(album);
            return MapAlbumToAlbumDto(addedAlbum);
        }
        public async Task UpdateAlbumAsync(AlbumDto albumDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var existingAlbum = await _albumRepository.GetByIdAsync(albumDto.Id);
            var userDetails = _httpContextAccessor.HttpContext.User;
            if (existingAlbum == null)
            {
                throw new KeyNotFoundException("No album found with this id");
            }

            if (existingAlbum.UserId != currentUserId && !userDetails.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("You do not have permission to edit this album.");
            }
            var album = MapOldWithNew(albumDto, existingAlbum);
            await _albumRepository.UpdateAsync(album);
        }
        public async Task DeleteAlbumAsync(AlbumDto albumDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var existingAlbum = await _albumRepository.GetByIdAsync(albumDto.Id);
            var userDetails = _httpContextAccessor.HttpContext.User;
            if (existingAlbum == null)
            {
                throw new KeyNotFoundException("No album found with this id");
            }

            if (existingAlbum.UserId != currentUserId && userDetails.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException("You dont have permission to delete this album");
            }
            await _albumRepository.DeleteAsync(existingAlbum);
        }
        public async Task AddPictureToAlbumAsync(int albumId, int pictureId)
        {
            await _albumRepository.AddPictureToAlbumAsync(albumId, pictureId);
        }

        public async Task RemovePictureFromAlbumAsync(int albumId, int pictureId)
        {
            await _albumRepository.RemovePictureFromAlbumAsync(albumId, pictureId);
        }
        private AlbumDto MapAlbumToAlbumDto(Album album)
        {
            if (album == null)
            {
                return null;
            }
            return new AlbumDto
            {
                Id = album.Id,
                Name = album.Name,
                isPrivate = album.isPrivate,
                PictureIds = (album.Pictures ?? Enumerable.Empty<Picture>()).Select(p => p.Id).ToList(),
            };
        }

        private Album MapAlbumDtoToAlbum(AlbumDto albumDto)
        {
            return new Album
            {
                Id = albumDto.Id,
                Name = albumDto.Name,
                isPrivate = albumDto.isPrivate,
            };
        }
        private Album MapOldWithNew(AlbumDto albumDto, Album existingAlbum)
        {
            if (albumDto.Name != null)
            {
                existingAlbum.Name = albumDto.Name;
            }
            if (albumDto.isPrivate != existingAlbum.isPrivate)
            {
                existingAlbum.isPrivate = albumDto.isPrivate;
            }
            return existingAlbum;
        }
    }
}
