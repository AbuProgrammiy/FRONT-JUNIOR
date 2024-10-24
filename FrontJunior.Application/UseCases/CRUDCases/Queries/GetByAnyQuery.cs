﻿using FrontJunior.Domain.Entities.Views;
using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Queries
{
    public class GetByAnyQuery:IRequest<ResponseModel>
    {
        public string Username { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string? ColumnValue { get; set; }
    }
}
