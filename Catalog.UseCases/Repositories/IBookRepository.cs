using System;
using System.Collections.Generic;
using System.Text;
using Catalog.Domain.Entities;

namespace Catalog.UseCases.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book book);
        Task SaveAsync();
    }
}
