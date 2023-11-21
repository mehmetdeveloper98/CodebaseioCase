namespace Shared
{
    static public class RabbitMQSettings
    {
        public const string Stock_OrderCreatedEventQueue = "stock-order-craeted-event-queue";
        public const string Order_StockNotReservedEventQueue = "order-stock-not-reserved-event-queue";
        public const string Stock_OrderFailedEventQueue = "order-stock-failed-event-queue";
        public const string Order_OrderCompletedEventQueue = "order-completed-event-queue";

    }
}
