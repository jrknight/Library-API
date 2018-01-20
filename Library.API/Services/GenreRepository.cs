using Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class GenreRepository : IGenreRepository
    {
        private LibraryDbContext _ctx;

        public GenreRepository(LibraryDbContext context)
        {
            _ctx = context;
        }

        public void AddGenre(Genre genre)
        {
            _ctx.Genres.Add(genre);
        }

        public bool GenreExists(int Id)
        {
            var genre = _ctx.Genres.FirstOrDefault(g => g.Id == Id);
            if (genre == null)
            {
                return false;
            }
            return true;
        }
        public bool GenreExists(string genreName)
        {
            var genre = _ctx.Genres.FirstOrDefault(g => g.Name == genreName);
            if (genre == null)
            {
                return false;
            }
            return true;
        }

        public Genre GetGenre(int Id)
        {
            return _ctx.Genres.FirstOrDefault(g => g.Id == Id);
        }

        public int GetGenreId(string genreName)
        {
            return _ctx.Genres.FirstOrDefault(g => g.Name.Equals(genreName)).Id;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _ctx.Genres.OrderBy(g => g.Name);
        }

        public IEnumerable<Genre> GetGenresWithIds(IEnumerable<int> Id)
        {
            List<Genre> GenreList = new List<Genre>();

            foreach(int i in Id)
            {
                GenreList.Concat(_ctx.Genres.Where(g => g.Id == i));
            }

            return GenreList;
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
