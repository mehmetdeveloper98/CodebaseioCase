using Customer.Application.CQRS.Commands;
using Customer.Application.Interfaces;
using MassTransit;
using MediatR;
using Shared.CommonModel;
using Shared.Events;
using System.Collections.Generic;

namespace Customer.Application.CQRS.Queries
{
    public class GetOrderByIdQuery : IRequest<ResponseModel<GetOrderByIdResponse>>
    {
        public int orderId;


        public GetOrderByIdQuery(int orderId)
        {
            this.orderId = orderId;
        }
        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ResponseModel<GetOrderByIdResponse>>
        {
            private readonly IOrderService _orderService;
            readonly IPublishEndpoint _publishEndpoint;

            public GetOrderByIdQueryHandler(IOrderService orderService, IPublishEndpoint publishEndpoint)
            {
                _orderService = orderService;
                _publishEndpoint = publishEndpoint;
            }

            public async Task<ResponseModel<GetOrderByIdResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
            {
                var response = await _orderService.GetOrderById(request.orderId);
                GetOrderByIdResponse convertedDto = new GetOrderByIdResponse //mapper kullanılabilirdi ama çok vakit olmadığından elle bind yaptım.
                {
                    CreatedDate = response.CreatedDate,
                    Customer = response.Customer,
                    CustomerId = response.CustomerId,
                    OrderItems = response.OrderItems,
                    OrderStatus = response.OrderStatus,
                    TotalPrice = response.TotalPrice
                };
                return ResponseModel<GetOrderByIdResponse>.Success(200, convertedDto);
            }
        }
    }
}
