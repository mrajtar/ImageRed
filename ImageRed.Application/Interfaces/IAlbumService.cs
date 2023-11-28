using ImageRed.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Interfaces
{
    public interface IAlbumService
    {
        Task<AlbumDto> GetAlbumAsync(int id);
        Task<AlbumDto> AddAlbumAsync(AlbumDto albumDto);
        Task UpdateAlbumAsync(AlbumDto albumDto);
        Task DeleteAlbumAsync(AlbumDto albumDto);
        Task AddPictureToAlbumAsync(int albumId, int pictureId);
        Task RemovePictureFromAlbumAsync(int albumId, int pictureId);
    }
}
