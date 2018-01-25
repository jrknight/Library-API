using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Student GetBookRequestStudent(int bookRequestId)
        {
            return ctx.BookRequests.FirstOrDefault(br => br.Id == bookRequestId).Student;
        }

        public bool Save()
        {
            return (ctx.SaveChanges() >= 0);
        }
    }
}
