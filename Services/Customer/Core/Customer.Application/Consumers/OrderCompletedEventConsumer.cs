using Customer.Application.Enums;
using Customer.Application.Interfaces;
using Customer.Domain.Entities;
using MassTransit;
using Shared.Events;

namespace Customer.Application.Consumers
{
    public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
    {
        readonly IOrderService _orderService;

        public OrderCompletedEventConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<OrderCompletedEvent> context)
        {
            Order order = await _orderService.GetOrderById(context.Message.OrderId);
            order.OrderStatus = (int)OrderStatus.Completed;
            await _orderService.UpdateOrder(order);
        }

    }
}
