using Customer.Application.Dtos;
using MediatR;
using Customer.Application.Interfaces;
using Shared.Events;
using Customer.Domain.Entities;
using MassTransit;
using Shared.CommonModel;
using Customer.Application.Enums;

namespace Customer.Application.CQRS.Commands
{
    public class CreateOrderCommand:IRequest<ResponseModel<NoContent>>
    {
        public CreateOrderModel createOrderModel;
        

        public CreateOrderCommand(CreateOrderModel createOrderModel)
        {
            this.createOrderModel = createOrderModel;
        }
        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseModel<NoContent>>
        {
            private readonly IOrderService _orderService;
            readonly IPublishEndpoint _publishEndpoint;

            public CreateOrderCommandHandler(IOrderService orderService, IPublishEndpoint publishEndpoint)
            {
                _orderService = orderService;
                _publishEndpoint = publishEndpoint;
            }

            public async Task<ResponseModel<NoContent>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                Order order = new()
                {
                    CustomerId = request.createOrderModel.CustomerId,
                    CreatedDate = DateTime.Now,
                    OrderStatus = (int)OrderStatus.Suspend
                };

                order.OrderItems = request.createOrderModel.OrderItems.Select(oi => new OrderItem
                {
                    Count = oi.Count,
                    Price = oi.Price,
                    ProductId = oi.ProductId,
                }).ToList();

                order.TotalPrice = request.createOrderModel.OrderItems.Sum(oi => (oi.Price * oi.Count));

                var response = await _orderService.CreateOrder(order);

                OrderCreatedEvent orderCreatedEvent = new()
                {
                    CustomerId = order.CustomerId,
                    OrderId = order.Id,
                    OrderItems = order.OrderItems.Select(oi => new Shared.Messages.OrderItemMessage
                    {
                        Count = oi.Count,
                        ProductId = oi.ProductId,
                    }).ToList(),
                    TotalPrice = order.TotalPrice
                };

                await _publishEndpoint.Publish(orderCreatedEvent);

                return ResponseModel<NoContent>.Success(201);
            }
        }
    }
}
