using Shared.Messages;

namespace Shared.Events
{
    public class OrderFailedEvent
    {
        public string OrderId { get; set; }
        public string Message { get; set; }
        public List<OrderItemMessage> OrderItems { get; set; }
    }
}
