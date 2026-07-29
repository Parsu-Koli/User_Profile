using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadForm_Project.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public byte[]? FileData { get; set; }

        // Store UTC
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public IFormFile? UploadFile { get; set; }
    }
}