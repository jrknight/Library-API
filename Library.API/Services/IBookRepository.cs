using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface IBookRepository 
    {
        IEnumerable<Book> GetBooks();
        Book GetBook(int Id);
        Author GetBookAuthor(int bookId);
        bool BookExists(string Name);
        bool BookExists(int Id);
        void AddBook(Book book);
        bool Save();
    }
}
