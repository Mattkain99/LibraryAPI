using System;

namespace LibraryAPI.Models
{
    public class LibraryBook
    {
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}