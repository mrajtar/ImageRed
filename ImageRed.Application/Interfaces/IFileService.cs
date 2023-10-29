using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Interfaces
{
        public interface IFileService
        {
            public Tuple<int, string> SaveImage(IFormFile imageFile);
            public bool DeleteImage(string imageFileName);
        }
}
