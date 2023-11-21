using Customer.Domain.Entities;

namespace Customer.Application.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int orderId);
        Task<List<Order>> GetOrderByCustomerId(int customerId);
        Task<List<Order>> GetAllOrder();
        Task<List<Order>> GetAllOrderByDate(DateTime startDate, DateTime enDate);
        Task<Order> CreateOrder(Order order);

        Task<bool> UpdateOrder(Order order);
    }
}
