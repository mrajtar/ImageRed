using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Dto
{
    public class PictureDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Tag { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
