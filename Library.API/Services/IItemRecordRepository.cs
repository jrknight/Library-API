using Entities;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IItemRecordRepository
    {
        IEnumerable<ItemRecord> GetItemRecords();
        List<ItemRecord> GetItemRecordsForUser(string UserId);
        bool ItemRecordExistsForUser(string UserId);
        void AddItemRecord(ItemRecord itemRecord);
        void DeleteItemRecordWithId(int itemId);
        void DeleteItemRecord(ItemRecordForDeletionDto itemRecord);
        bool Save();
    }
}
