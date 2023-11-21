using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using Customer.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            return order;
        }

        public async Task<List<Order>> GetOrderByCustomerId(int customerId)
        {
            var order = _context.Orders.Where(x => x.CustomerId == customerId).ToList();
            return order;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrder()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<List<Order>> GetAllOrderByDate(DateTime startDate, DateTime endDate)
        {
            return await _context.Orders.Include(x=>x.OrderItems).Where(x=>x.CreatedDate< endDate && x.CreatedDate>startDate).ToListAsync();
        }
    }
}
