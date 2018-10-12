using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Library.API.Services
{
    public class ItemRequestRepository : IItemRequestRepository
    {
        private ReCircleDbContext ctx;

        public ItemRequestRepository(ReCircleDbContext context)
        {
            ctx = context;
        }

        public void AddItemRequest(ItemRequest bookRequest)
        {
            ctx.ItemRequests.Add(bookRequest);
        }

        public bool ItemRequestExistsForUser(string UserId)
        {
            return ctx.ItemRequests.Any(br => br.UserId == UserId);
        }

        public bool ItemRequestExistsForUserAndItem(string UserId, int bookId)
        {
            return ctx.ItemRequests.Any(br => br.UserId == UserId && br.ItemId == bookId);
        }

        public IEnumerable<ItemRequest> GetItemRequests()
        {
            return ctx.ItemRequests.ToList();
        }

        public List<ItemRequest> GetItemRequestsForUser(string UserId)
        {
            var bookrequests = ctx.ItemRequests.Where(br => br.UserId == UserId).ToList();
            return bookrequests;
        }

        public void RemoveItemRequest(string userId, int bookId)
        {
            ItemRequest b = ctx.ItemRequests.Where(br => br.UserId == userId && br.ItemId == bookId).FirstOrDefault();
            ctx.ItemRequests.Remove(b);
        }

        public bool Save()
        {
            return (ctx.SaveChanges() >= 0);
        }
    }
}
