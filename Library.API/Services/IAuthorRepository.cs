using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthor(int Id);
        IEnumerable<Book> GetBooksWithAuthor(int authorId);
        bool AuthorExists(int Id);
        void AddAuthor(Author author);
        bool Save();
    }
}
