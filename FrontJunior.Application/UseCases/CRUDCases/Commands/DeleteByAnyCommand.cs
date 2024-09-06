using FrontJunior.Domain.Entities.Models;
using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Commands
{
    public class DeleteByAnyCommand:IRequest<ResponseModel>
    {
        public string SecurityKey { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
