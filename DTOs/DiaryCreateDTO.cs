using MyBlog.API.MongoDB.Models;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.API.DTOs
{
    public class DiaryCreateDTO
    {
        [Required]
        public string? DiaryName { get; set; }
        public string? DiaryDescription { get; set; }
        public string? DiaryImage { get; set; }
    }
}
