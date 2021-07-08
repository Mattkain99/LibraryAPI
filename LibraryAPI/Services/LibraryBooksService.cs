using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Data.Repositories;
using LibraryAPI.Dtos;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    public class LibraryBooksService
    {
        private readonly LibraryBookRepository _libraryBookRepository;

        public LibraryBooksService(LibraryBookRepository libraryBookRepository)
        {
            _libraryBookRepository = libraryBookRepository;
        }

        public async Task<IReadOnlyCollection<LibraryDto>> GetLibrariesAsync() => 
            (await _libraryBookRepository.GetAllLibrariesAsync())
            .Select(l => new LibraryDto{ Name = l.Name })
            .ToList();

        public async Task<IReadOnlyCollection<LibraryDto>> GetLibrariesWithBooksAsync()
        {
            var libraries = await _libraryBookRepository.GetAllLibrariesAsync();
            var librariesByBooks = (await _libraryBookRepository.GetLibrariesByBookAsync())
                .Select(dict => new BookDto
                {
                    Title = dict.Key,
                    BookCopiesByLibraries = dict.Value
                        .GroupBy(l => l.Name)
                        .Select(g => new BookCopiesByLibrary { Copies = g.ToList().Count, LibraryName = g.Key})
                        .ToList()
                }).ToList();
                
            return libraries
                .Select(l => new LibraryDto
                {
                    Name = l.Name,
                    Books = librariesByBooks.Where(b => b.BookCopiesByLibraries.Any(c => c.LibraryName == l.Name)).ToList()
                })
                .ToList();
        }

        public async Task<IReadOnlyCollection<BookDto>> SearchBooksByNameAsync(string clue)
        {
            var booksByLibrary = await _libraryBookRepository.GetLibrariesByBookAsync(clue);
            return booksByLibrary.Select(dict => new BookDto
            {
                Title = dict.Key,
                BookCopiesByLibraries = dict.Value
                    .GroupBy(l => l.Name)
                    .Select(g => new BookCopiesByLibrary { Copies = g.ToList().Count, LibraryName = g.Key})
                    .ToList()
            }).ToList();
        }

        public async Task AddBookAsync(Book book)
        {
            await _libraryBookRepository.AddBookAsync(book);
        }
        
        public async Task AddLibraryAsync(Library library)
        {
            await _libraryBookRepository.AddLibraryAsync(library);
        }
        
        public async Task AddLibraryBookAsync(Guid libraryId, Guid bookId)
        {
            await _libraryBookRepository.AddLibraryBookAsync(libraryId, bookId);
        }
    }
}