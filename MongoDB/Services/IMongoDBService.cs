using MyBlog.API.DTOs;
using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.MongoDB.Services
{
    public interface IMongoDBService<T> where T : class
    {
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync();
        Task CreateItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(Guid id);
    }
}