namespace MyBlog.API.MongoDB.Models
{
    public record Diary
    {
        public Guid Id { get; init; }
        public string? DiaryName { get; set; }
        public string? DiaryDescription { get; set; }
        public string? DiaryImage { get; set; }
        public List<Guid> DiaryEntries { get; set; } = new List<Guid>();
        public DateTimeOffset CreatedDate { get; init; }
    }
}