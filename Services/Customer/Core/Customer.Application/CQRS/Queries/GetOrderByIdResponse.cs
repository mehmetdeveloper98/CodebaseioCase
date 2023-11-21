using Customer.Domain.Entities;

namespace Customer.Application.CQRS.Queries
{
    public class GetOrderByIdResponse
    {
        public int CustomerId { get; set; }
        public Domain.Entities.Customer Customer { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
