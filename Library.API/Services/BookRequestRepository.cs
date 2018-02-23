using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Library.Entities;

namespace Library.API.Services
{
    public class BookRequestRepository : IBookRequestRepository
    {
        private LibraryDbContext ctx;

        public BookRequestRepository(LibraryDbContext context)
        {
            ctx = context;
        }

        public void AddBookRequest(BookRequest bookRequest)
        {
            ctx.BookRequests.Add(bookRequest);
        }

        public bool BookRequestExists(int Id)
        {
            return ctx.BookRequests.Any(br => br.Id == Id);
        }

        public BookRequest GetBookRequest(int Id)
        {
            return ctx.BookRequests.FirstOrDefault(br => br.Id == Id);
        }

        public IEnumerable<BookRequest> GetBookRequests()
        {
            return ctx.BookRequests.ToList();
        }

        public List<BookRequest> GetBookRequestsForUser(string UserName)
        {
            return ctx.BookRequests.Where(br => br.User.UserName == UserName).ToList();
        }

        public LibraryUser GetBookRequestStudent(int bookRequestId)
        {
            return ctx.BookRequests.FirstOrDefault(br => br.Id == bookRequestId).User;
        }

        public bool Save()
        {
            return (ctx.SaveChanges() >= 0);
        }
    }
}
