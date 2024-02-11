using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using datapac_interview.Presistance;

namespace datapac_interview.Services;

public class BookService(IBookRepository bookRepository): IBookService
{
    
    public async Task<List<Book>> UpdateBooksWithOrder(List<UpdateOrderedBookRequest> requestedBooks)
    {
        var bookIds = requestedBooks.Select(s => s.Id).ToList();
        var existingBooks = await bookRepository.GetBooksByIdsAsync(bookIds);
        
        List<Book> updateData = new List<Book>();
        foreach (var book in existingBooks)
        {
            book.Available = requestedBooks.FirstOrDefault(s => s.Id == book.Id).Available;
            updateData.Add(book);
        }

        await bookRepository.UpdateBatchAsync(updateData);
        return updateData;
    }
    
    public async Task<List<Book>> UpdateBooksWithOrder(List<Book> requestedBooks)
    {
        var bookIds = requestedBooks.Select(s => s.Id).ToList();
        var existingBooks = await bookRepository.GetBooksByIdsAsync(bookIds);
        
        List<Book> updateData = new List<Book>();
        foreach (var book in existingBooks)
        {
            book.Available = true;
            updateData.Add(book);
        }

        await bookRepository.UpdateBatchAsync(updateData);
        return updateData;
    }
    

    public async Task<List<Book>> GetBooks(List<Guid> ids)
    {
        var existingBooks = await bookRepository.GetBooksByIdsAsync(ids);
        return await bookRepository.GetBooksByIdsAsync(ids);
    }

}