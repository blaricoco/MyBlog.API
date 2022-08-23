using System.ComponentModel.DataAnnotations;

namespace MyBlog.API.DTOs
{
    public class DiaryUpdateDTO
    {
        [Required]
        public string? DiaryName { get; set; }
        public string? DiaryDescription { get; set; }
        public string? DiaryImage { get; set; }
    }
}
