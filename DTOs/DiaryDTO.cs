using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.DTOs
{
    public class DiaryDTO
    {
        public Guid Id { get; init; }
        public string? DiaryName { get; set; }
        public string? DiaryDescription { get; set; }
        public string? DiaryImage { get; set; }
        public List<DiaryEntryDTO> DiaryEntries { get; set; } = new List<DiaryEntryDTO>();
        public DateTimeOffset CreatedDate { get; init; }
    }
}
