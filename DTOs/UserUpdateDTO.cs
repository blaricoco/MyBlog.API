using System.ComponentModel.DataAnnotations;

namespace MyBlog.API.DTOs
{
    public class UserUpdateDTO
    {
        [Required]
        public string? FirstName { get; init; }

        [Required]
        public string? LastName { get; init; }
        public string? Email { get; set; }
    }
}
