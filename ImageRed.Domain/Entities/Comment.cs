using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ImageRed.Domain.Entities
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public int PictureId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}