using FrontJunior.Application.UseCases.DataStorageCases.Commands;
using FrontJunior.Application.UseCases.DataStorageCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrontJunior.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataStorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DataStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{page}/{count}")]
        public async Task<IEnumerable<DataStorage>> GetAll(int page,int count)
        {
            return await _mediator.Send(new GetAllDataStorageQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{page}/{count}")]
        public async Task<IEnumerable<DataStorage>> GetAllColumns(int page, int count)
        {
            return await _mediator.Send(new GetAllDataStorageColumnsQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DataStorage> GetById(Guid id)
        {
            return await _mediator.Send(new GetDataStorageByIdQuery { Id = id });
        }

        [HttpGet]
        [Route("{userId}/{tableName}")]
        public async Task<IEnumerable<string>> GetColumnsByTableName(Guid userId, string tableName)
        {
            return await _mediator.Send(new GetColumnsByTableNameQuery { UserId = userId, TableName=tableName }) ;
        }

        [HttpPost]
        public async Task<ResponseModel> Create(CreateDataStorageCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpPut]
        public async Task<ResponseModel> Update(UpdateDataStorageCommand request)
        {
            return await _mediator.Send(request);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteDataStorageCommand {  Id = id });
        }
    }
}
