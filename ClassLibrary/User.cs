using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;


namespace Entities
{
    public class User : IdentityUser
    {

        public string Name { get; set; }
        public int Credit { get; set; }

        public IEnumerable<ItemRequest> ItemRequests { get; set; }
        public IEnumerable<ItemRecord> ItemRecords { get; set; }
        public IEnumerable<Item> Items { get; set; }
        
    }
}
