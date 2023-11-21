using Customer.Application.Interfaces;
using MediatR;
using Shared.CommonModel;

namespace Customer.Application.CQRS.Queries
{
    public class GetOrderByDateQuery:IRequest<ResponseModel<List<GetOrderByIdResponse>>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetOrderByDateQuery(DateTime endDate, DateTime startDate)
        {
            EndDate = endDate;
            StartDate = startDate;
        }

        public class GetOrderByDateQueryHandler : IRequestHandler<GetOrderByDateQuery, ResponseModel<List<GetOrderByIdResponse>>>
        {
            private readonly IOrderService _orderService;

            public GetOrderByDateQueryHandler(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task<ResponseModel<List<GetOrderByIdResponse>>> Handle(GetOrderByDateQuery request, CancellationToken cancellationToken)
            {
                var orderList = await _orderService.GetAllOrderByDate(request.StartDate, request.EndDate);
                if (orderList == null) return null;
                var responseModel = orderList.Select(k => new GetOrderByIdResponse
                {
                    CreatedDate = k.CreatedDate,
                    Customer = k.Customer,
                    CustomerId = k.CustomerId,
                    OrderItems = k.OrderItems,
                    OrderStatus = k.OrderStatus,
                    TotalPrice = k.OrderStatus
                }).ToList();

                return ResponseModel<List<GetOrderByIdResponse>>.Success(200, responseModel);
            }
        }
    }
}
