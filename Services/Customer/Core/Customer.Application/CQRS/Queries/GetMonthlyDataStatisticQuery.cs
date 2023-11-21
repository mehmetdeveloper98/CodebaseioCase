using Customer.Application.Dtos;
using Customer.Application.Interfaces;
using MediatR;
using Shared.CommonModel;

namespace Customer.Application.CQRS.Queries
{
    public class GetMonthlyDataStatisticQuery:IRequest<ResponseModel<MonthlyStatisticDto>>
    {
        public class GetMonthlyDataStatisticQueryHandler : IRequestHandler<GetMonthlyDataStatisticQuery, ResponseModel<MonthlyStatisticDto>>
        {
            private readonly IOrderService _orderService;

            public GetMonthlyDataStatisticQueryHandler(IOrderService orderService)
            {
                _orderService = orderService;
            }

            public async Task<ResponseModel<MonthlyStatisticDto>> Handle(GetMonthlyDataStatisticQuery request, CancellationToken cancellationToken)
            {
                var orderList = await _orderService.GetAllOrder();
                var responseModel = new MonthlyStatisticDto()
                {
                    TotalOrder = orderList.Count,
                    TotalBookCount = orderList.Select(x => x.OrderItems.Count).First(),
                    TotalPurchasedAmount = orderList.Sum(x => x.TotalPrice)
                };

                return ResponseModel<MonthlyStatisticDto>.Success(200, responseModel);
            }
        }
    }
}
