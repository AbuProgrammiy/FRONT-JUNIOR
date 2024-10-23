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
        [Route("{securityKey}/{tableName}")]
        public async Task<object> GetAll(string securityKey,string tableName,int? page,int? count)
        {
            return await _mediator.Send(new GetAllQuery
            {
                SecurityKey = securityKey,
                TableName = tableName,
                Page = page,
                Count = count
            });
        }

        [HttpGet]
        [Route("{securityKey}/{tableName}/{columnName}/{columnValue}")]
        public async Task<object> GetByAny(string securityKey, string tableName, string columnName, string columnValue)
        {
            return await _mediator.Send(new GetByAnyQuery
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue
            });
        }
        
        [HttpGet]
        [Route("{securityKey}/{tableName}/{columnName}")]
        public async Task<object> GetByAny(string securityKey, string tableName, string columnName)
        {
            return await _mediator.Send(new GetByAnyQuery
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName
            });
        }

        [HttpPost]
        [Route("{securityKey}/{tableName}")]
        public async Task<ResponseModel> Create(string securityKey, string tableName, object body)
        {
            return await _mediator.Send(new CreateCommand
            {
                SecurityKey = securityKey,
                TableName = tableName,
                Body = body,
            });
        }

        [HttpPut]
        [Route("{securityKey}/{tableName}/{columnName}/{columnValue}")]
        public async Task<ResponseModel> UpdateByAny(string securityKey, string tableName, string columnName, string columnValue, object body)
        {
            return await _mediator.Send(new UpdateByAnyCommand
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue,
                Body = body,
            });
        }
        
        [HttpPut]
        [Route("{securityKey}/{tableName}/{columnName}")]
        public async Task<ResponseModel> UpdateByAny(string securityKey, string tableName, string columnName, object body)
        {
            return await _mediator.Send(new UpdateByAnyCommand
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName,
                Body = body,
            });
        }

        [HttpDelete]
        [Route("{securityKey}/{tableName}/{columnName}/{columnValue}")]
        public async Task<ResponseModel> DeleteByAny(string securityKey, string tableName, string columnName, string columnValue)
        {
            return await _mediator.Send(new DeleteByAnyCommand
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName,
                ColumnValue = columnValue
            });
        }
        
        [HttpDelete]
        [Route("{securityKey}/{tableName}/{columnName}")]
        public async Task<ResponseModel> DeleteByAny(string securityKey, string tableName, string columnName)
        {
            return await _mediator.Send(new DeleteByAnyCommand
            {
                SecurityKey = securityKey,
                TableName = tableName,
                ColumnName = columnName
            });
        }
    }
}
