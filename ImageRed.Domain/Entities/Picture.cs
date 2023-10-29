using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImageRed.Domain.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile ImageUrl { get; set; }
        public string Tag { get; set; } = string.Empty;

        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
