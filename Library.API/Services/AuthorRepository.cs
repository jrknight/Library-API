using Library.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        /// <summary>
        /// This class requires that all checking for existence/validity is done on the controller.
        /// </summary>
        
        private LibraryDbContext _ctx;

        public AuthorRepository(LibraryDbContext context)
        {
            _ctx = context;
        }

        public void AddAuthor(Author author)
        {
            _ctx.Authors.Add(author);
        }

        public bool AuthorExists(int Id)
        {
            var book = _ctx.Authors.Where(a => a.Id == Id);
            if (book == null)
            {
                return false;
            }
            return true;
        }

        public Author GetAuthor(int Id)
        {
            return _ctx.Authors.Where(a => a.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _ctx.Authors.OrderBy(a => a.Name).ToList();
        }

        public IEnumerable<Book> GetBooksWithAuthor(int authorId)
        {
            return _ctx.Books.Where(b => b.AuthorId == authorId).ToList();
        }

        public bool Save()
        {
            return (_ctx.SaveChanges() >= 0);
        }
    }
}
