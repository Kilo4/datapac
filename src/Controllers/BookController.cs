using AutoMapper;
using datapac_interview.Dto.Book.request;
using datapac_interview.Models;
using datapac_interview.Presistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models.Context;

namespace datapac_interview.Controllers;

[Route("api/Book/")]
[ApiController]
public class BookController(AppDbContext context, IMapper mapper, IBookRepository bookRepository) : ControllerBase
{
    /// <summary>
    /// Create new book
    /// </summary>
    /// <response code="201">Returns the newly created book</response>
    /// <response code="400">If the item is null</response>
    /// <response code="409">If the item all ready exist </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Book>> CreateBook([FromBody] CreateBookRequest bookRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var exist = await bookRepository.CheckByTitleAndAuthorAsync(bookRequest.Title, bookRequest.Author);
        if (exist == true)
            return Conflict("Book exists");
            
        
        var item = mapper.Map<CreateBookRequest, Book>(bookRequest);
        await bookRepository.CreateAsync(item);
        
        return CreatedAtAction(nameof(CreateBook), new { id = item.Id }, item);
    }
    
    [HttpPatch("{Id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Book>> UpdateBook([FromBody] UpdateBookRequest book, Guid Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = await bookRepository.GetByIdAsync(Id);
        if (result == null)
            return NotFound($"Book {Id} not found");

        var exist = await bookRepository.CheckByTitleAndAuthorAsync(book.Title, book.Author);
        if (exist == true)
            return Conflict("Book exists");

        await bookRepository.UpdateAsync(result, book);
        
        return CreatedAtAction(nameof(GetBook), new { id = Id }, result);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Book>> GetBook(Guid Id)
    {
        var book = await bookRepository.GetByIdAsync(Id);
        if (book == null)
            return NotFound($"Book {Id} not found");
        

        return Ok(book);
    }
    
    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteBook(Guid Id)
    {
        var book = await bookRepository.GetByIdAsync(Id);
        if (book == null)
            return NotFound($"Book {Id} not found");
        
        await bookRepository.DeleteAsync(book);

        return Ok("Deleted"); 
    }

}