using datapac_interview.Dto.Book.request;
using datapac_interview.Models;

namespace datapac_interview.Services;

public interface IBookService
{
    Task<List<Book>> GetBooks(List<Guid> ids);
    Task<List<Book>> UpdateBooksWithOrder(List<UpdateOrderedBookRequest> requestedBooks);
    Task<List<Book>> UpdateBooksWithOrder(List<Book> requestedBooks);
    

}