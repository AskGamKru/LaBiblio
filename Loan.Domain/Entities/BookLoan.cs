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
        public DateTime CreatedAt { get; private set; }
        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }
        public string? CancellationReason { get; private set; }
        private BookLoan() { }

        public BookLoan(Guid bookId, Guid userId)
        {
            Id = Guid.NewGuid();
            BookId = bookId;
            UserId = userId;
            Status = LoanStatus.Created;
            CreatedAt = DateTime.UtcNow;
        }

        public void Confirm()
        {
            if (Status != LoanStatus.Created)
            {
                throw new DomainException("Only created loans can be confirmed.");
            }
            Status = LoanStatus.Confirmed; 
            ConfirmedAt = DateTime.UtcNow;
        }

        public void Cancel(string reason)
        {
            if (Status == LoanStatus.Cancelled)
            {
                throw new DomainException("Already cancelled.");
            }
            Status = LoanStatus.Cancelled;
            CancelledAt = DateTime.UtcNow;
            CancellationReason = reason;
        }
    }
}
