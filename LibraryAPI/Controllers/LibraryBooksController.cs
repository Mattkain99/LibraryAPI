using System;
using System.Threading.Tasks;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("/api/")]
    public class LibraryBooksController : ControllerBase
    {
        private readonly LibraryBooksService _libraryBooksService;

        public LibraryBooksController(LibraryBooksService libraryBooksService)
        {
            _libraryBooksService = libraryBooksService;
        }

        [HttpGet("libraries")]
        public async Task<IActionResult> GetLibrariesAsync()
        {
            var libraries = await _libraryBooksService.GetLibrariesAsync();
            return Ok(libraries);
        }
        
        [HttpGet("libraries/books")]
        public async Task<IActionResult> GetBooksAsync()
        {
            var librariesWithBooks = _libraryBooksService.GetLibrariesWithBooksAsync();
            return Ok(librariesWithBooks);
        }
        
        [HttpGet("libraries/books/{clue}")]
        public async Task<IActionResult> SearchBooksByNameAsync(string clue)
        {
            var booksMatching = await _libraryBooksService.SearchBooksByNameAsync(clue);
            return Ok(_libraryBooksService);
        }
        
        [HttpPost("/libraries")]
        public async Task<IActionResult> AddBookAsync([FromBody]Book book)
        {
            await _libraryBooksService.AddBookAsync(book);
            return Ok();
        }
        
        [HttpPost("/libraries/books")]
        public async Task<IActionResult> AddLibraryAsync([FromBody]Library library)
        {
            await _libraryBooksService.AddLibraryAsync(library);
            return Ok();
        }
        
        [HttpPost("/libraries/{libraryId}/books/{bookId}")]
        public async Task<IActionResult> AddLibraryBookAsync(Guid libraryId, Guid bookId)
        {
            await _libraryBooksService.AddLibraryBookAsync(libraryId, bookId);
            return Ok();
        }
    }
}