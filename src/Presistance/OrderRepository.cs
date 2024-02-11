using datapac_interview.Dto.Order.request;
using datapac_interview.Models;
using datapac_interview.Services;
using Microsoft.EntityFrameworkCore;
using MyApi.Models.Context;

namespace datapac_interview.Presistance;

public class OrderRepository(AppDbContext context, IBookService bookService): IOrderRepository
{
    public async Task<Order> GetOrderById(Guid id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(w => w.Id == id);
        return order;
    }

    public async Task UpdateOrder(Order order)
    {
        order.Completed = true;
        await context.SaveChangesAsync();
    }

    public async Task<Order> CreateOrder(List<Book> items, CreateOrderRequest request)
    {
        var order = new Order()
        {
            UserMailAdress = request.UserMailAdress,
            Books = items
        };
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetBooksDueTomorrowAsync()
    {
        DateTime tomorrow = DateTime.Today.AddDays(1);

        var orders =await context.Orders.Where(w => w.LastDate.Date == tomorrow.Date).ToListAsync();
        return orders;
    }
}