namespace Shared.Events
{
    public class StockNotReservedEvent
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }
    }
}
