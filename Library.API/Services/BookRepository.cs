using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookRepository : IBookRepository
    {
        /// <summary>
        /// This class requires that all checking for existence/validity is done on the controller.
        /// </summary>
        private LibraryDbContext _ctx;

        public BookRepository(LibraryDbContext context)
        {
            _ctx = context;
        }

        public async Task<Book> GetBookAsync(int Id)
        {
            return await _ctx.Books.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _ctx
                .Books
                .Include(m => m.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .OrderBy(b => b.Title)
                .ToListAsync();
            
        }

        public async Task<bool> BookExistsAsync(string Name)
        {
            return await _ctx.Books.AnyAsync(b => b.Title == Name);
            
        }

        public async Task AddBookAsync(Book book)
        {
            await _ctx.Books.AddAsync(book);
            _ctx.Authors.FirstOrDefault(a => a.Id == book.AuthorId).BooksWritten.Add(book);
            
        }

        public async Task<Author> GetBookAuthorAsync(int bookId)
        {
            return await _ctx.Authors.Where(a => a.BooksWritten.Select(b => b.Id == bookId).FirstOrDefault()).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _ctx.SaveChangesAsync() >= 0);
        }

        public async Task<bool> BookExistsAsync(int Id)
        {
            return await _ctx.Books.AnyAsync(b => b.Id == Id);
        }

        public async Task UpdateBookAsync(Book book)
        {
            Book bookInDb = _ctx.Books.Where(b => b.Id == book.Id).FirstOrDefault();
            if (bookInDb != null)
            {
                bookInDb.ISBN = book.ISBN;
                bookInDb.AuthorId = book.AuthorId;
                bookInDb.Author = book.Author;
                bookInDb.BookGenres = book.BookGenres;
                bookInDb.Summary = book.Summary;
                bookInDb.Title = book.Title;

                await _ctx.SaveChangesAsync();
            }

        }
    }
}
