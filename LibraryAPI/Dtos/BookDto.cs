using System.Collections.Generic;

namespace LibraryAPI.Dtos
{
    public class BookDto
    {
        public string Title { get; set; }
        public List<BookCopiesByLibrary> BookCopiesByLibraries { get; set; }
    }

    public class BookCopiesByLibrary
    {
        public int Copies { get; set; }

        public string LibraryName { get; set; }
    }
}