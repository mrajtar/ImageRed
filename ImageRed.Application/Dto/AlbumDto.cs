using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Dto
{
    public class AlbumDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isPrivate { get; set; }
        public List<int>? PictureIds { get; set; }
    }
}
