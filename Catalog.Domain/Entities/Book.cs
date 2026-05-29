using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        private Book() { }

        public Book (string title, string author)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
        }
    }
}
