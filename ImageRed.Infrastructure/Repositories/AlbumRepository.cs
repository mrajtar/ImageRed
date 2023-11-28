using ImageRed.Domain.Entities;
using ImageRed.Domain.Interfaces;
using ImageRed.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImageRed.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ImageRedDbContext _context;

        public AlbumRepository(ImageRedDbContext context)
        {
            _context = context;
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            var album = await _context.Albums.SingleOrDefaultAsync(x => x.Id == id);
            var pictures = _context.Pictures.Where(p => p.AlbumId == id).ToList();
            album.Pictures = pictures;

            return album;
        }

        public async Task<Album> AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
            await _context.SaveChangesAsync();
            return album;
        }

        public async Task UpdateAsync(Album album)
        {

            _context.Albums.Update(album);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Album album)
        {
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
        }

        public async Task AddPictureToAlbumAsync(int albumId, int pictureId)
        {
            var album = await _context.Albums.FindAsync(albumId);
            var picture = await _context.Pictures.FindAsync(pictureId);
            if (album == null)
            {
                throw new KeyNotFoundException($"No album found with id {albumId}");
            }
            if (picture == null)
            {
                throw new KeyNotFoundException($"No picture found with id {pictureId}");
            }
            if (album.Pictures == null)
            {
                album.Pictures = new List<Picture>();
            }
            album.Pictures.Add(picture);
            await _context.SaveChangesAsync();

        }

        public async Task RemovePictureFromAlbumAsync(int albumId, int pictureId)
        {
            var album = await _context.Albums.FindAsync(albumId);
            var picture = await _context.Pictures.FindAsync(pictureId);
            if (album == null)
            {
                throw new KeyNotFoundException($"No album found with id {albumId}");
            }
            if (picture == null)
            {
                throw new KeyNotFoundException($"No picture found with id {pictureId}");
            }
            if (!album.Pictures.Contains(picture))
            {
                throw new InvalidOperationException($"The picture with id {pictureId} is not in the album with id {albumId}");
            }
            album.Pictures.Remove(picture);
            await _context.SaveChangesAsync();

        }
    }
}
