﻿using FrontJunior.Application.UseCases.TableCases.Commands;
using FrontJunior.Application.UseCases.TableCases.Queries;
using FrontJunior.Domain.Entities.Models.PrimaryModels;
using FrontJunior.Domain.Entities.Views;
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
        public async Task<ResponseModel> GetAll(int page, int count)
        {
            return await _mediator.Send(new GetAllTableQuery { Page = page, Count = count });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseModel> GetById(Guid id)
        {
            return await _mediator.Send(new GetTableByIdQuery { Id = id });
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ResponseModel> GetTablesByUserId(Guid userId)
        {
            return await _mediator.Send(new GetTablesByUserIdQuery
            {
                UserId = userId
            });
        }

        [HttpGet]
        [Route("{userId}/{tableName}")]
        public async Task<ResponseModel> IsTableNameExistsByUserId(Guid userId, string tableName)
        {
            return await _mediator.Send(new IsTableNameExistsByUserIdQuery { UserId = userId, TableName = tableName });
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
        [Route("{userId}/{tableName}")]
        public async Task<ResponseModel> TruncateTableByUserId(Guid userId, string tableName)
        {
            return await _mediator.Send(new TruncateTableByUserIdCommand { UserId = userId, TableName = tableName });
        }
        
        [HttpDelete]
        [Route("{userId}/{tableName}")]
        public async Task<ResponseModel> DeleteTableByUserId(Guid userId, string tableName)
        {
            return await _mediator.Send(new DeleteTableByUserIdCommand { UserId = userId, TableName = tableName });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseModel> Delete(Guid id)
        {
            return await _mediator.Send(new DeleteTableCommand { Id = id });
        }
    }
}
