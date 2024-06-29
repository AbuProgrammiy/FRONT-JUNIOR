using MediatR;

namespace FrontJunior.Application.UseCases.CRUDCase.Queries
{
    public class GetByAny:IRequest<object>
    {
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    }
}
