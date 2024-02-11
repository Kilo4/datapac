using System.Data.Entity;
using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using Microsoft.EntityFrameworkCore;
using MyApi.Models.Context;

namespace datapac_interview.Presistance;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public async Task<Book?> GetByIdAsync(Guid id)
    {
        var result = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        return result;
    }
    
    public async Task<List<Book>> GetBooksByIdsAsync(List<Guid> ids)
    {
        var books = await context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();
        return books;
    }
    
    public async Task<Book> GetByTitleAndAuthorAsync(string title, string author)
    {
        var result = await context.Books.SingleOrDefaultAsync(b => b.Author == author && b.Title == title);
        return result;
    }
    
    public async Task<bool> CheckByTitleAndAuthorAsync(string title, string author)
    {
        return context.Books.AsQueryable().Any(b => b.Author == author && b.Title == title);
    }
    

    public async Task CreateAsync(Book book)
    {
        book.Available = true;
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book oldBook ,UpdateBookRequest book)
    {
        oldBook.Author = book.Author ?? oldBook.Author;
        oldBook.Title = book.Title ?? oldBook.Title;
        oldBook.Price = book.Price ?? oldBook.Price;
        oldBook.Available = book.Available ?? oldBook.Available;
        await context.SaveChangesAsync();
    }
    
    public async Task UpdateBatchAsync(List<Book> books)
    {
        context.Books.UpdateRange(books);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        context.Books.Remove(book);
        context.SaveChangesAsync();
    }
}