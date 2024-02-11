using datapac_interview.Dto.Order.request;
using datapac_interview.Models;

namespace datapac_interview.Presistance;

public interface IOrderRepository
{
    Task<Order> GetOrderById(Guid id);
    Task<Order> CreateOrder(List<Book> items, CreateOrderRequest request);
    Task UpdateOrder(Order order);
    Task<List<Order>> GetBooksDueTomorrowAsync();
}