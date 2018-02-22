using Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class BookGenreRepository : IBookGenreRepository
    {
        private LibraryDbContext _ctx;
        
        public BookGenreRepository(LibraryDbContext context)
        {
            _ctx = context;
        }

        public void AddBookGenres(IEnumerable<BookGenre> bookGenres)
        {
            foreach (BookGenre bg in bookGenres)
            {
                _ctx.BookGenre.Add(bg);
            }
        }

        public bool GenreExists(string genreName)
        {
            var genre = _ctx.Genres.Where(g => g.Name.Equals(genreName));
            if (genre == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Book> GetBooksWithGenre(int genreId)
        {
            var bookGenre = _ctx.BookGenre.Where(bg => bg.GenreId == genreId).OrderBy(bg => bg.BookId).ToList();

            List<Book> Result = new List<Book>();

            foreach (BookGenre bg in bookGenre)
            {
                Result.Add(_ctx.Books.FirstOrDefault(b => b.Id == bg.BookId));
            }

            return Result;
        }

        public IEnumerable<Genre> GetGenresForBook(int bookId)
        {
            var bookGenre = _ctx.BookGenre.Where(bg => bg.BookId == bookId).OrderBy(bg => bg.GenreId).ToList();

            List<Genre> Result = new List<Genre>();

            foreach (BookGenre bg in bookGenre)
            {
                Result.Add(_ctx.Genres.FirstOrDefault(g => g.Id == bg.GenreId));
            }

            return Result;
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
