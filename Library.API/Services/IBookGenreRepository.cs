using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface IBookGenreRepository
    {
        bool GenreExists(string genreName);
        IEnumerable<Book> GetBooksWithGenre(int genreId);
        IEnumerable<Genre> GetGenresForBook(int bookId);
        void AddBookGenres(IEnumerable<BookGenre> bookGenres);
        bool Save();
    }
}
