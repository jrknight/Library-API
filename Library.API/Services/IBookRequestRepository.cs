using Entities;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IBookRequestRepository
    {
        IEnumerable<BookRequest> GetBookRequests();
        BookRequest GetBookRequest(int Id);
        LibraryUser GetBookRequestStudent(int bookRequestId);
        List<BookRequest> GetBookRequestsForUser(string UserName);
        bool BookRequestExists(int Id);
        void AddBookRequest(BookRequest bookRequest);
        bool Save();
    }
}
