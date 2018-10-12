using Entities;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IItemRequestRepository
    {
        IEnumerable<ItemRequest> GetItemRequests();
        List<ItemRequest> GetItemRequestsForUser(string UserId);
        bool ItemRequestExistsForUser(string UserId);
        bool ItemRequestExistsForUserAndItem(string UserId, int itemId);
        void RemoveItemRequest(string userId, int itemId);
        void AddItemRequest(ItemRequest itemRequest);
        bool Save();
    }
}
