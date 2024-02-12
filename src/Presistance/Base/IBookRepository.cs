using datapac_interview.Dto.Book.request;
using datapac_interview.Models;

namespace datapac_interview.Presistance;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetBooksByIdsAsync(List<Guid> ids);
    Task<Book?> GetByTitleAndAuthorAsync(string title, string author);
    Task<bool> CheckByTitleAndAuthorAsync(string title, string author);
    Task CreateAsync(Book book);
    Task<Book> UpdateAsync(Book oldbook, UpdateBookRequest book);
    Task UpdateBatchAsync(List<Book> books);
    Task DeleteAsync(Book book);
}