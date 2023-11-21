namespace Shared.Events
{
    public class StockReservedEvent
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
