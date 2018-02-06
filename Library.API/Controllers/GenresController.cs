using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/books/genres")]
    public class GenresController : Controller
    {
        private IBookGenreRepository bookGenreRepository;
        private IGenreRepository genreRepository;
        private IBookRepository bookRepository;

        public GenresController(IBookGenreRepository bgRepo, 
            IBookRepository bRepo, 
            IGenreRepository gRepo)
        {
            bookGenreRepository = bgRepo;
            genreRepository = gRepo;
            bookRepository = bRepo;
        }

        #region GetGenres


        [HttpGet("{genreName}")]
        public IActionResult GetBooksWithGenre(string genreName)
        {
            if (bookGenreRepository.GenreExists(genreName))
            {
                int genreId = genreRepository.GetGenreId(genreName);
                var results = Mapper.Map<IEnumerable<Book>>(bookGenreRepository.GetBooksWithGenre(genreId));
                return Json(results);
            }
            return BadRequest("The specified genre could not be found.");
        }

        [HttpGet("")]
        public IActionResult GetGenres()
        {
            return Json(genreRepository.GetGenres());
        }

        [HttpGet("{bookId}")]
        public IActionResult GetGenresForBook(int bookId)
        {
            if (bookRepository.BookExists(bookId))
            {
                var results = Mapper.Map<IEnumerable<Genre>>(bookGenreRepository.GetGenresForBook(bookId));
                return Json(results);
            }

            return BadRequest("The Book ID provided does not exist.");
        }

        #endregion

        #region PostGenres

        [HttpPost("")]
        public IActionResult NewGenre([FromBody] GenreForCreationDto genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            if (genreRepository.GenreExists(genre.Name))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var finalGenre = Mapper.Map<Entities.Genre>(genre);

            genreRepository.AddGenre(finalGenre);

            if (!genreRepository.Save())
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            var createdGenreToReturn = Mapper.Map<Models.GenreDto>(finalGenre);

            return Created($"api/books/{createdGenreToReturn.Name}", createdGenreToReturn);
        }

        #endregion

    }
}
