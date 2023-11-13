namespace ImageRed.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}