using AutoMapper;
using Library.API.Filters;
using Library.API.Models;
using Library.API.Services;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/books")]
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
        public async Task<IActionResult> GetBooksAsync(bool includeAuthors = false)
        {
            return Json(await bookRepository.GetBooksAsync());
        }

        [HttpGet("/{BookId}")]
        public async Task<IActionResult> GetBookAsync([FromHeader] int BookId)
        {

            var book = await bookRepository.GetBookAsync(BookId);

            if (book == null)
            {
                return BadRequest();
            }

            return Json(book);
        }

        #endregion


        #region PostBooks


        [HttpPost("")]
        public async Task<IActionResult> NewBookAsync([FromBody] BookForCreationDto book)
        {
            if (await bookRepository.BookExistsAsync(book.Title))
            {
                return BadRequest("Book already exists");
            }

            if (book.Title == book.Summary)
            {
                ModelState.AddModelError("Summary", "The title and summary must be separate values.");
            }

            if (book.genreIds == null)
            {
                return BadRequest("A genre value is required");
            }

            var finalBook = Mapper.Map<Entities.Book>(book);

            await bookRepository.AddBookAsync(finalBook);

            var genres = genreRepository.GetGenresWithIds(book.genreIds);

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

            if (!await bookRepository.SaveAsync())
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }



            var createdBookToReturn = Mapper.Map<Models.BookDto>(finalBook);

            return Created($"api/books/{createdBookToReturn.Id}", createdBookToReturn);
        }

        #endregion


        #region PutBooks


        [HttpPut("")]
        public async Task<IActionResult> PutBookAsync([FromBody] Book model)
        {
            if (await bookRepository.BookExistsAsync(model.Id))
            {

            }


            return Ok();
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
