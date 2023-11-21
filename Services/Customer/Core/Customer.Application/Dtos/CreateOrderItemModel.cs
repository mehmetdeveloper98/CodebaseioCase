namespace Customer.Application.Dtos
{
    public class CreateOrderItemModel
    {

        public string ProductId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}
