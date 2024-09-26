using Microsoft.EntityFrameworkCore;
using SoapApi.Infrastructure;
using SoapApi.Mappers;
using SoapApi.Models;

namespace SoapApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly RelationalDbContext _dbContext;

    public BookRepository(RelationalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        return book.ToModel();
    }

    public async Task<BookModel> CreateAsync(BookModel book, CancellationToken cancellationToken)
    {
        var bookEntity = book.ToEntity();
        bookEntity.Id = Guid.NewGuid();
        await _dbContext.AddAsync(bookEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return bookEntity.ToModel();
    }
}
