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
        Task<PictureDto> AddPictureAsync(PictureDto pictureDto, string UserId);
        Task UpdatePictureAsync(int id, PictureDto pictureDto, string UserId);
        Task DeletePictureAsync(int id, string UserId);
    }
}
