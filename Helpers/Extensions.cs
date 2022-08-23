using MyBlog.API.DTOs;
using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.Helpers
{
    public static class Extensions
    {
        public static UserDTO AsDTO(this User item)
        {
            return new UserDTO
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                CreatedDate = item.CreatedDate
            };
        }

        public static DiaryEntryDTO AsDTO(this DiaryEntry item)
        {
            return new DiaryEntryDTO
            {
                Id = item.Id,
                DiaryEntryTitle = item.DiaryEntryTitle,
                DiaryEntryDescription = item.DiaryEntryDescription,
                DiaryEntryType = item.DiaryEntryType,
                CreatedDate = item.CreatedDate,
                DiaryId = item.DiaryId

            };
        }

        public static DiaryDTO AsDTO(this Diary item)
        {
            return new DiaryDTO
            {
                Id = item.Id,
                DiaryName = item.DiaryName,
                DiaryDescription = item.DiaryDescription,
                DiaryImage = item.DiaryImage,
                CreatedDate = item.CreatedDate

            };
        }
    }
}
