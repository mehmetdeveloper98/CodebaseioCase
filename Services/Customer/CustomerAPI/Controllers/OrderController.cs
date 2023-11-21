using Customer.Application.CQRS.Commands;
using Customer.Application.CQRS.Queries;
using Customer.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateOrder(CreateOrderModel createOrderModel)
        {
            return CreateActionResultInstance(await _mediator.Send(new CreateOrderCommand(createOrderModel)));
        }

        [HttpPost]
        [Route("GetOrderById")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            return CreateActionResultInstance(await _mediator.Send(new GetOrderByIdQuery(orderId)));
        }


        [HttpPost]
        [Route("GetOrderByDate")]
        public async Task<IActionResult> GetOrderByDate(DateTime startDate, DateTime endDate)
        {
            return CreateActionResultInstance(await _mediator.Send(new GetOrderByDateQuery(startDate,endDate)));
        }
    }
}
