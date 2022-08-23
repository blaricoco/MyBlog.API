namespace MyBlog.API.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; init; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}
