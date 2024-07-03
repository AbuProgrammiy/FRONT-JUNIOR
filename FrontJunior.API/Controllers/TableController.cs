using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities;
using FrontJunior.Domain.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrontJunior.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{page}/{count}")]
        public async Task<IEnumerable<Table>> GetAll(int page, int count)
        {
            return await _mediator.Send(new GetAllTableQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Table> GetById(Guid id)
        {
            return await _mediator.Send(new GetTableByIdQuery { Id = id });
        }

        [HttpGet]
        [Route("{userId}/{page}/{count}")]
        public async Task<IEnumerable<Table>> GetTablesByUserId(Guid userId, int page, int count)
        {
            return await _mediator.Send(new GetTablesByUserIdQuery
            {
                UserId = userId,
                Page = page,
                Count = count
            });
        }

        [HttpPost]
        public async Task<ResponseModel> Create(CreateTableCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<ResponseModel> Update(UpdateTableCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteTableCommand { Id = id });
        }
    }
}
