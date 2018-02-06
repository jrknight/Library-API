using AutoMapper;
using Library.API.Filters;
using Library.API.Models;
using Library.API.Services;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.API.Controllers
{
    [Route("api/books")]
    [Authorize]
    public class BooksController : Controller
    {
        private IBookRepository bookRepository;
        private IBookGenreRepository bookGenreRepository;
        private IGenreRepository genreRepository;
        private IBookRequestRepository bookRequestRepository;

        public BooksController(IBookRepository bcontext, 
            IBookGenreRepository bgContext, 
            IGenreRepository gContext, 
            IBookRequestRepository brContext)
        {
            bookRepository = bcontext;
            bookGenreRepository = bgContext;
            genreRepository = gContext;
            bookRequestRepository = brContext;
        }

        #region GetBooks

        [HttpGet("")]
        public IActionResult GetBooks(bool includeAuthors = false)
        {
            return Json(bookRepository.GetBooks());
        }

        [HttpGet("/{BookId}")]
        public IActionResult GetBook([FromHeader] int BookId)
        {

            var book = bookRepository.GetBook(BookId);

            if (book == null)
            {
                return BadRequest();
            }

            return Json(book);
        }

        #endregion

        #region PostBooks


        [HttpPost("")]
        public IActionResult NewBook([FromBody] BookForCreationDto book, [FromBody] IEnumerable<int> genreIds)
        {
            if (bookRepository.BookExists(book.Title))
            {
                return BadRequest("Book already exists");
            }

            if (book.Title == book.Summary)
            {
                ModelState.AddModelError("Summary", "The title and summary must be separate values.");
            }

            var finalBook = Mapper.Map<Entities.Book>(book);

            bookRepository.AddBook(finalBook);

            var genres = genreRepository.GetGenresWithIds(genreIds);

            List<BookGenre> BookGenres = new List<BookGenre>();

            foreach (Genre g in genres)
            {
                BookGenres.Add(new BookGenre
                {
                    Book = finalBook,
                    BookId = finalBook.Id,
                    Genre = g,
                    GenreId = g.Id
                });
            }

            bookGenreRepository.AddBookGenres(BookGenres);

            if (!bookRepository.Save())
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }



            var createdBookToReturn = Mapper.Map<Models.BookDto>(finalBook);

            return Created($"api/books/{createdBookToReturn.Id}", createdBookToReturn);
        }

        #endregion

        #region PostBookRequests

        [HttpPost("requests")]
        public IActionResult RequestNewBook([FromBody] BookRequestForCreationDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalRequest = Mapper.Map<BookRequest>(request);

            bookRequestRepository.AddBookRequest(finalRequest);

            return Created($"api/books/{finalRequest.Id}", finalRequest);
        }

        #endregion

        #region GetBookRequests

        [HttpGet("requests/{Id}")]
        public IActionResult GetBookRequest(int Id)
        {
            if (bookRequestRepository.BookRequestExists(Id))
            {
                return Json(bookRequestRepository.GetBookRequest(Id));
            }
            return BadRequest("Book Request doesn't exist.");
        }

        [HttpGet("requests")]
        public IActionResult GetBookRequests()
        {
            return Json(bookRequestRepository.GetBookRequests());
        }

        #endregion

    }
}
