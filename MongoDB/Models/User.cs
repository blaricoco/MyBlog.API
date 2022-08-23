using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlog.API.MongoDB.Models
{
    public record User
    {
        public Guid Id { get; init; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public List<Diary>? Diaries { get; set; } 
    }
}