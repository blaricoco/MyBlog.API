using System.ComponentModel.DataAnnotations;

namespace MyBlog.API.DTOs
{
    public class DiaryEntryCreateDTO
    {
        [Required]
        public string? DiaryEntryTitle { get; set; }
        public string? DiaryEntryDescription { get; set; }
        [Required]
        public string? DiaryEntryType { get; set; }
    }
}
