using System.Web.Http.ModelBinding;
using AutoMapper;
using datapac_interview.Dto.Book.request;
using datapac_interview.Dto.Order.request;
using datapac_interview.Models;
using datapac_interview.Presistance;
using datapac_interview.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Models.Context;
namespace datapac_interview.Controllers;
[Route("api/Order/")]
[ApiController]
public class OrderController(AppDbContext context, IMapper mapper, IOrderRepository orderRepository, IBookService bookService): ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Order>> createOrder([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var items = mapper.Map<List<UpdateOrderedBookRequest>, List<Book>>(request.Books);
        var updatedBooks = await bookService.UpdateBooksWithOrder(request.Books);
        
        var order = await orderRepository.CreateOrder(updatedBooks, request);
        
        return CreatedAtAction(nameof(createOrder), new { id = order.Id }, order);
    }
    
    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<Order>> updateOrder(Guid Id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var order = await orderRepository.GetOrderById(Id);
        if (order == null)
            Conflict($"Order with id: {Id}, not found");

        var books = await bookService.GetBooks(order.Books.AsEnumerable().Select(w => w.Id).ToList());
        await bookService.UpdateBooksWithOrder(books);
        await orderRepository.UpdateOrder(order);
 
        return Ok();
    }
    
}