using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class ItemRepository : IItemRepository
    {
        /// <summary>
        /// This class requires that all checking for existence/validity is done on the controller.
        /// </summary>
        private ReCircleDbContext _ctx;
        private UserManager<User> userManager;

        public ItemRepository(ReCircleDbContext context, UserManager<User> userMgr)
        {
            _ctx = context;
            userManager = userMgr;
        }

        public async Task<Item> GetItemAsync(int Id)
        {
            return await _ctx.Items.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            
            return await _ctx
                .Items
                .Include(m => m.OwnerEmail)
                .Include(b => b.ItemRecords)
                .OrderBy(b => b.Title)
                .ToListAsync();
            
        }

        public async Task<bool> ItemExistsAsync(string Name)
        {
            return await _ctx.Items.AnyAsync(b => b.Title == Name);
            
        }

        public async Task AddItemAsync(Item item)
        {
            await _ctx.Items.AddAsync(item);

            
        }

        public async Task<bool> SaveAsync()
        {
            return (await _ctx.SaveChangesAsync() >= 0);
        }

        public async Task<bool> ItemExistsAsync(int Id)
        {
            return await _ctx.Items.AnyAsync(b => b.Id == Id);
        }

        public async Task<User> GetItemOwnerAsync (int Id)
        {
            var item = await _ctx.Items.Where(b => b.Id == Id).FirstOrDefaultAsync();
            return await userManager.FindByEmailAsync(item.OwnerEmail);
        }

        public async Task UpdateItemAsync(Item item)
        {
            Item bookInDb = _ctx.Items.Where(b => b.Id == item.Id).FirstOrDefault();
            if (bookInDb != null)
            {
                bookInDb.PhotoUrl = item.PhotoUrl;
                bookInDb.OwnerEmail = item.OwnerEmail;
                bookInDb.CurrentHolderEmail = item.CurrentHolderEmail;
                bookInDb.Description = item.Description;
                bookInDb.Title = item.Title;

                await _ctx.SaveChangesAsync();
            }

        }
    }
}
