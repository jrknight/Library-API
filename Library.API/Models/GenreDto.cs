using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Models
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
