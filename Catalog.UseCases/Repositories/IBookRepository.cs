using Catalog.Domain.Entities;

namespace Catalog.UseCases.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id);
        Task AddAsync(Book book);
        Task SaveAsync();
    }
}
