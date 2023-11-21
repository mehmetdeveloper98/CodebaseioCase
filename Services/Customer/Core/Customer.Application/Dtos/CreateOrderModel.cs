namespace Customer.Application.Dtos
{
    public class CreateOrderModel
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemModel> OrderItems { get; set; }
    }
}
