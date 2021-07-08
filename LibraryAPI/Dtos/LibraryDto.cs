using System.Collections.Generic;

namespace LibraryAPI.Dtos
{
    public class LibraryDto
    {
        public string Name { get; set; }
        public IReadOnlyCollection<BookDto> Books { get; set; }
    }
}