using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Library.API.Models;

namespace Library.API.Services
{
    public class ItemRecordRepository : IItemRecordRepository
    {
        private ReCircleDbContext ctx;

        public ItemRecordRepository(ReCircleDbContext context)
        {
            ctx = context;
        }

        public void AddItemRecord(ItemRecord bookRecord)
        {
            ctx.ItemRecords.Add(bookRecord);
        }

        public bool ItemRecordExistsForUser(string UserId)
        {
            return ctx.ItemRecords.Any(br => br.UserId == UserId);
        }

        public void DeleteItemRecord(ItemRecordForDeletionDto itemRecord)
        {
            var record = ctx.ItemRecords.Where(b => b.ItemId == itemRecord.BookId && b.UserId == itemRecord.UserId).FirstOrDefault();
            ctx.ItemRecords.Remove(record);
        }

        public void DeleteItemRecordWithId(int itemId)
        {
            var x = ctx.ItemRecords.FirstOrDefault(b => b.Item.Id == itemId);
            ctx.ItemRecords.Remove(x);
        }

        public IEnumerable<ItemRecord> GetItemRecords()
        {
            return ctx.ItemRecords.ToList();
        }

        public List<ItemRecord> GetItemRecordsForUser(string UserId)
        {
            return ctx.ItemRecords.Where(br => br.UserId == UserId).ToList();
        }

        public bool Save()
        {
            return (ctx.SaveChanges() >= 0);
        }
    }
}
