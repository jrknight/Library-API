using Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.API
{
    public static class BookContextExtensions
    {
        public static void EnsureSeedDataForContext(this LibraryDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            var books = new List<Book>()
            {
                new Book()
                {
                    Title = "Harry Potter and the Chamber of Secrets",
                    Summary = "A book involving Harry Potter.",
                    AuthorId = 1,
                    Id = 0
                }
            };

            context.AddRange(books);
            context.SaveChanges();

        }
    }
}
