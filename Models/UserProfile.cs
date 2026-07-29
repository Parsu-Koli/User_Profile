using Microsoft.AspNetCore.Http;
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

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^[0-9]{10}$")]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public string Address { get; set; }

        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public byte[]? FileData { get; set; }

        [NotMapped]
        public IFormFile? UploadFile { get; set; }
    }
}