using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCases.Queries
{
    public class GetByAnyQuery:IRequest<object>
    {
        public string SecurityKey { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
