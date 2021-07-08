using System;
using System.Collections.Generic;

namespace LibraryAPI.Models
{
    public class Library
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
        public List<LibraryBook> LibraryBooks { get; set; }
        public List<Book> Books { get; set; }
    }
}