using Customer.Application.Enums;
using Customer.Domain.Entities;

namespace Customer.Application.CQRS.Queries
{
    public class GetOrdersQueryResponse
    {
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
