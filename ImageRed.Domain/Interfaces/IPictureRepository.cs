using ImageRed.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Domain.Interfaces
{
    public interface IPictureRepository
    {
        Task<IEnumerable<Picture>> GetAllAsync();
        Task<Picture> GetByIdAsync(int id);
        Task<Picture> AddAsync(Picture picture);
        Task UpdateAsync(Picture picture);
        Task DeleteAsync(Picture picture);

    }
}
