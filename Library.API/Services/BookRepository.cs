using System.Collections.Generic;
using System.Linq;
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

        public Book GetBook(int Id)
        {
            return _ctx.Books.Where(b => b.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Book> GetBooks()
        {
            return _ctx
                .Books
                .Include(m => m.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .OrderBy(b => b.Title)
                .ToList();
            
        }

        public bool BookExists(string Name)
        {
            return _ctx.Books.Any(b => b.Title == Name);
            
        }

        public void AddBook(Book book)
        {
            _ctx.Books.Add(book);
            _ctx.Authors.FirstOrDefault(a => a.Id == book.AuthorId).BooksWritten.Add(book);
        }

        public Author GetBookAuthor(int bookId)
        {
            return _ctx.Authors.Where(a => a.BooksWritten.Select(b => b.Id == bookId).FirstOrDefault()).FirstOrDefault();
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }

        public bool BookExists(int Id)
        {
            return _ctx.Books.Any(b => b.Id == Id);
        }
    }
}
