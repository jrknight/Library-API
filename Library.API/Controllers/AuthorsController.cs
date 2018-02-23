using AutoMapper;
using Library.API.Models;
using Library.API.Services;
using Library.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Controllers
{
    [Authorize]
    [Route("api/authors")]
    public class AuthorsController : Controller
    {
        private IAuthorRepository authorRepository;

        public AuthorsController(IAuthorRepository repo)
        {
            authorRepository = repo;
        }

        [HttpGet("")]
        public IActionResult GetAuthors()
        {
            var authors = authorRepository.GetAuthors();
            var results = Mapper.Map<IEnumerable<AuthorDto>>(authors);

            return Json(results);
        }

        [HttpGet("{Id}")]
        public IActionResult GetAuthor(int Id)
        {
            var author = authorRepository.GetAuthor(Id);
            if (author == null)
            {
                return BadRequest();
            }
            author.BooksWritten = authorRepository.GetBooksWithAuthor(author.Id).ToList();

            return Json(author);
        }

        [HttpPost("")]
        public IActionResult NewAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var finalAuthor = Mapper.Map<Author>(author);

            authorRepository.AddAuthor(finalAuthor);

            if (!authorRepository.Save())
            {
                return StatusCode(500, "A problem occurred while handling your request.");
            }

            var createdAuthorToReturn = Mapper.Map<AuthorDto>(finalAuthor);

            return Created($"api/authors/{createdAuthorToReturn.Id}", createdAuthorToReturn);

        }

        [HttpGet("{authorId}/books")]
        public IActionResult GetBooksWithAuthor(int authorId)
        {
            if (authorRepository.AuthorExists(authorId))
            {
                return Json(authorRepository.GetBooksWithAuthor(authorId));
            }
            return BadRequest();
        }

    }
}
