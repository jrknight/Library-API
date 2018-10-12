using Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.API
{
    public static class BookContextExtensions
    {
        public static void EnsureSeedDataForContext(this ReCircleDbContext context)
        {
            if (context.Items.Any())
            {
                return;
            }

            var books = new List<Item>()
            {
                new Item()
                {
                    Title = "New item",
                    Description = "its an item",
                    Id = 0
                }
            };

            context.AddRange(books);
            context.SaveChanges();

        }
    }
}
