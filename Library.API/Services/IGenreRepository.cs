using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Services
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
        Genre GetGenre(int Id);
        int GetGenreId(string genreName);
        IEnumerable<Genre> GetGenresWithIds(IEnumerable<int> Id);
        bool GenreExists(int Id);
        bool GenreExists(string genreName);
        void AddGenre(Genre genre);
        bool Save();
    }
}
