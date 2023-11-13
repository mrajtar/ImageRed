using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImageRed.Domain.Entities
{
    public class Picture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? Tag { get; set; } = string.Empty;
        public string UserId { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Comment> Comments { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
