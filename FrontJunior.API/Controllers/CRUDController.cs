using FrontJunior.Application.UseCases.CRUDCases.Commands;
using FrontJunior.Application.UseCases.CRUDCases.Queries;
using FrontJunior.Domain.Entities.Views;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FrontJunior.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CRUDController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{username}/{tableName}")]
        public async Task<object> GetAll(string username, string tableName,int? page,int? count)
        {
            return await _mediator.Send(new GetAllQuery
            {
                Username = username,
                TableName = tableName,
                Page = page,
                Count = count
            });
        }

        [HttpGet]
        [Route("{username}/{tableName}/{columnName}/{columnValue}")]
        public async Task<object> GetByAny(string username, string tableName, string columnName, string columnValue)
        {
            return await _mediator.Send(new GetByAnyQuery
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue
            });
        }
        
        [HttpGet]
        [Route("{username}/{tableName}/{columnName}")]
        public async Task<object> GetByAny(string username, string tableName, string columnName)
        {
            return await _mediator.Send(new GetByAnyQuery
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName
            });
        }

        [HttpPost]
        [Route("{username}/{tableName}")]
        public async Task<ResponseModel> Create(string username, string tableName, object body)
        {
            return await _mediator.Send(new CreateCommand
            {
                Username = username,
                TableName = tableName,
                Body = body,
            });
        }

        [HttpPut]
        [Route("{username}/{tableName}/{columnName}/{columnValue}")]
        public async Task<ResponseModel> UpdateByAny(string username, string tableName, string columnName, string columnValue, object body)
        {
            return await _mediator.Send(new UpdateByAnyCommand
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue,
                Body = body,
            });
        }
        
        [HttpPut]
        [Route("{username}/{tableName}/{columnName}")]
        public async Task<ResponseModel> UpdateByAny(string username, string tableName, string columnName, object body)
        {
            return await _mediator.Send(new UpdateByAnyCommand
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName,
                Body = body,
            });
        }

        [HttpDelete]
        [Route("{username}/{tableName}/{columnName}/{columnValue}")]
        public async Task<ResponseModel> DeleteByAny(string username, string tableName, string columnName, string columnValue)
        {
            return await _mediator.Send(new DeleteByAnyCommand
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue
            });
        }
        
        [HttpDelete]
        [Route("{username}/{tableName}/{columnName}")]
        public async Task<ResponseModel> DeleteByAny(string username, string tableName, string columnName)
        {
            return await _mediator.Send(new DeleteByAnyCommand
            {
                Username = username,
                TableName = tableName,
                ColumnName = columnName
            });
        }
    }
}
