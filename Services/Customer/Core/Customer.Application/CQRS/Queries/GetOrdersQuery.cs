using Customer.Application.Dtos.Common;
using Customer.Application.Enums;
using Customer.Application.Interfaces;
using MediatR;
using Shared.CommonModel;

namespace Customer.Application.CQRS.Queries
{
    public class GetOrdersQuery : IRequest<ResponseModel<List<GetOrdersQueryResponse>>>
    {
        public int CustomerId;

        public GetOrdersQuery(int CustomerId)
        {
            this.CustomerId = CustomerId;
        }
        public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, ResponseModel<List<GetOrdersQueryResponse>>>
        {
            readonly IOrderService _orderService;

            public GetOrdersQueryHandler(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task<ResponseModel<List<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
            {
                var orders = await _orderService.GetOrderByCustomerId(request.CustomerId);
                if (orders == null) return null;
                var response = orders.Select(x => new GetOrdersQueryResponse
                {
                    CreatedDate = x.CreatedDate,
                    OrderItems = x.OrderItems,
                    OrderStatus = (OrderStatus)x.OrderStatus,
                    TotalPrice = x.TotalPrice
                }).ToList();

                return ResponseModel<List<GetOrdersQueryResponse>>.Success(200, response);

            }
        }
    }
}
