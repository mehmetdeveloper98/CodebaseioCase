using Customer.Application.Enums;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using MassTransit;
using Shared.Events;

namespace Customer.Application.Consumers
{

    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        readonly IOrderService _orderService;

        public StockNotReservedEventConsumer( IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            Order order = await _orderService.GetOrderById(context.Message.OrderId);
            order.OrderStatus = (int)OrderStatus.Failed;
            await _orderService.UpdateOrder(order);
        }
    }
}
