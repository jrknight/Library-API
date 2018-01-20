using Library.Entities;
using System.Collections.Generic;

namespace Library.API.Models
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Book> BooksWritten { get; set; } = new List<Book>();
    }
}
