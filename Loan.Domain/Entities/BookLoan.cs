using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.Domain.Entities
{
    public class BookLoan
    {
        public Guid Id { get; private set; }
        public Guid BookId { get; private set; }
        public Guid UserId { get; private set; }

        public LoanStatus Status { get; private set; }

        public DateTime LoanedAt { get; private set; }
        public DateTime? DueDate { get; private set; }

        private BookLoan() { }

        public BookLoan(Guid bookId, Guid userId)
        {
            Id = Guid.NewGuid();
            BookId = bookId;
            UserId = userId;
            Status = LoanStatus.Created;
            LoanedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            Status = LoanStatus.Active;
        }

        public void Reject()
        {
            Status = LoanStatus.Cancelled;
        }
    }
}
