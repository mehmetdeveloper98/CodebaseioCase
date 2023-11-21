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
    public class CustomerController : CustomBaseController
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return CreateActionResultInstance(await _mediator.Send(new RegisterCommand(registerDto)));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return CreateActionResultInstance(await _mediator.Send(new LoginQuery(loginDto)));
        }

        [HttpGet]
        [Authorize]
        [Route("GetOrders")]
        public async Task<IActionResult> GetOrders(int customerId)
        {
            return CreateActionResultInstance(await _mediator.Send(new GetOrdersQuery(customerId)));
        }
    }
}
