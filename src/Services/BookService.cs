using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using datapac_interview.Presistance;
using Microsoft.AspNetCore.Http.HttpResults;

namespace datapac_interview.Services;

public class BookService(IBookRepository bookRepository): IBookService
{
    
    public async Task<List<Book>> UpdateBooksWithOrder(List<UpdateOrderedBookRequest> requestedBooks)
    {
        var bookIds = requestedBooks.Select(s => s.Id).ToList();
        var existingBooks = await bookRepository.GetBooksByIdsAsync(bookIds);
        var conflictBooks = existingBooks.Where(b => b.Available != true).ToList();
        if (conflictBooks.Count > 0)
        {
            var conflictBookIds = string.Join(", ", conflictBooks.Select(book => book.Id));
            throw new Exception($"Books are already booked: {conflictBookIds}");
        }

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