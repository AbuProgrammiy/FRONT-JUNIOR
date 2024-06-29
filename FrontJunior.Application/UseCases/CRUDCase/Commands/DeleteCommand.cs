﻿using FrontJunior.Domain.Entities.DTOs;
using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCase.Commands
{
    public class DeleteCommand:IRequest<ResponseModel>
    {
        public Guid SecurityKey { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
