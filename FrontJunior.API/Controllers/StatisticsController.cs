using FrontJunior.Application.UseCases.StatisticsCases.Queries;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrontJunior.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseModel> GetBasicStstistics()
        {
            return await _mediator.Send(new GetBasicStatisticsQuery());
        }
    }
}
