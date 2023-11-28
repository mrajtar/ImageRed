using ImageRed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Domain.Interfaces
{
    public interface IAlbumRepository
    {
        Task<Album> GetByIdAsync(int id);
        Task<Album> AddAsync(Album album);
        Task UpdateAsync(Album album);
        Task DeleteAsync(Album album);
        Task AddPictureToAlbumAsync(int albumId, int pictureId);
        Task RemovePictureFromAlbumAsync(int albumId, int pictureId);
    }
}
