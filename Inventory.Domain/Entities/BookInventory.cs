using Inventory.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Domain.Entities
{
    public class BookInventory
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Author { get; private set; } = string.Empty;
        public int TotalQuantity { get; private set; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        public byte[] RowVersion { get; private set; } = [];

        private BookInventory() { }

        public BookInventory(Guid bookId, string title, string author, int totalQuantity) 
        {
            Id = Guid.NewGuid();
            BookId = bookId;
            Title = title;
            Author = author;
            TotalQuantity = totalQuantity;
            AvailableQuantity = totalQuantity;
            ReservedQuantity = 0;
        }
        public void Reserve()
        {
            if (AvailableQuantity <= 0)
            {
                throw new DomainException("No copies available.");
            }
            AvailableQuantity--;
            ReservedQuantity++;
        }
        public void Release()
        {
            if (AvailableQuantity <= 0)
            {
                throw new DomainException("No reserved copies to release.");
            }
            AvailableQuantity++;
            ReservedQuantity--;
        }
    }
}
