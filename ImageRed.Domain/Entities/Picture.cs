using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Author { get; set; } = string.Empty;
        public bool isPrivate { get; set; }
        public int AlbumId { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
