using Customer.Application.CQRS.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticController : CustomBaseController
    {
        private readonly IMediator mediator;

        public StatisticController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyData()
        {
            return CreateActionResultInstance(await mediator.Send(new GetMonthlyDataStatisticQuery()));
        }
    }
}
