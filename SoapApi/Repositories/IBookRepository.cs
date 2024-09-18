using SoapApi.Models;

namespace SoapApi.Repositories;

public interface IBookRepository
{
    Task<BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<BookModel> CreateAsync(BookModel book, CancellationToken cancellationToken);
}
