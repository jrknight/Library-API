using Library.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IBookRepository 
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int Id);
        Task<Author> GetBookAuthorAsync(int bookId);
        Task<bool> BookExistsAsync(string Name);
        Task<bool> BookExistsAsync(int Id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task<bool> SaveAsync();
    }
}
