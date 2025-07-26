namespace DatingAppCourse.Api.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public string PublicId { get; set; } = string.Empty;
        // Optional: Add a property for the user ID if needed
        // public int UserId { get; set; }
    }
}