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

        public async Task<Dictionary<string, List<Library>>> GetLibrariesByBookAsync(string clue = null)
        {
            var query = _context.Set<LibraryBook>()
                .Include(lb => lb.Library)
                .Include(lb => lb.Book)
                .AsQueryable();
                
            if (!string.IsNullOrEmpty(clue))
            {
                query = query.Where(lb => lb.Book.Title.ToLower().Contains(clue.ToLower()));
            }
                
            return await query
                .GroupBy(lb => lb.Book.Title)
                .ToDictionaryAsync(
                g => g.Key,
                g => g.Select(lb => lb.Library).ToList());
        }
    }
}