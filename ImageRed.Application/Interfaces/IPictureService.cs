using ImageRed.Application.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Interfaces
{
    public interface IPictureService
    {
        Task<IEnumerable<PictureDto>> GetAllPicturesAsync();
        Task<PictureDto> GetPictureAsync(int id);
        Task<PictureDto> AddPictureAsync(PictureDto pictureDto);
        Task UpdatePictureAsync(int id, PictureDto pictureDto);
        Task DeletePictureAsync(int id);
    }
}
