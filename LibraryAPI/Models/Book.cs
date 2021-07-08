using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAPI.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Library> Libraries => LibraryBooks.Select(lb => lb.Library).ToList();

        public List<LibraryBook> LibraryBooks { get; set; }
    
    }
}