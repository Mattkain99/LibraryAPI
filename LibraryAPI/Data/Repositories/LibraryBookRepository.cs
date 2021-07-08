using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data.Repositories
{
    public class LibraryBookRepository
    {
        private readonly ApplicationDbContext _context;

        public LibraryBookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Book>> GetBooksAsync() =>
            await _context.Set<Book>().Include(b => b.Libraries).ToListAsync();

        public async Task AddBookAsync(Book book)
        {
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddLibraryAsync(Library library)
        {
            await _context.AddAsync(library);
            await _context.SaveChangesAsync();
        }
        
        public async Task AddLibraryBookAsync(Guid libraryId, Guid bookId)
        {
            await _context.AddAsync(new LibraryBook
            {
                LibraryId = libraryId,
                BookId = bookId
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Library>> GetAllLibrariesAsync() =>
            await _context.Set<Library>().ToListAsync();

        public async Task<Dictionary<Library, List<Book>>> GetAllLibraryBookAsync() =>
            await _context.Set<LibraryBook>()
                .Include(lb => lb.Book)
                .ThenInclude(b=>b.LibraryBooks)
                .ThenInclude(lb=>lb.Library)
                .Include(lb => lb.Library)
                .GroupBy(lb=>lb.Library)
                .ToDictionaryAsync(g=>g.Key, g=>g.Select(lb=>lb.Book).ToList());

        public async Task<IReadOnlyCollection<Book>> SearchBookAsync(string clue) =>
            await _context.Set<Book>()
                .Include(b => b.LibraryBooks)
                .ThenInclude(lb => lb.Library)
                .Where(b => b.Name.ToLower().Contains(clue.ToLower()))
                .ToListAsync();
    }
}