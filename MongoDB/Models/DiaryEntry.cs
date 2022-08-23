namespace MyBlog.API.MongoDB.Models
{
    public record DiaryEntry
    {
        public Guid Id { get; init; }
        public string? DiaryEntryTitle { get; set; }
        public string? DiaryEntryDescription { get; set; }
        public string? DiaryEntryType { get; set; }
        public DateTimeOffset CreatedDate { get; init; }
        public Guid DiaryId { get; set; }

    }
}
