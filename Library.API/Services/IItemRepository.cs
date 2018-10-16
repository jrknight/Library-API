using Entities;
using Library.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IItemRepository 
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemAsync(int Id);
        Task<User> GetItemOwnerAsync(int itemId);
        Task<bool> ItemExistsAsync(int Id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task<bool> SaveAsync();
    }
}
