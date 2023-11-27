using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRed.Application.Dto
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int PictureId { get; set; }
        public string? Text { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
