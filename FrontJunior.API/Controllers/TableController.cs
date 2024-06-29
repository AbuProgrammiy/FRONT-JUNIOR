using FrontJunior.Application.UseCases.UserCases.Commands;
using FrontJunior.Application.UseCases.UserCases.Queries;
using FrontJunior.Domain.Entities.DTOs;
using FrontJunior.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Application.UseCases.TableCases.Commands;

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
