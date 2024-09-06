using FrontJunior.Domain.Entities;
using MediatR;

namespace FrontJunior.Application.UseCases.TableCases.Queries
{
    public class GetAllTableQuery:IRequest<IEnumerable<Table>>
    {
        public int Page { get; set; }
        public int Count { get; set; }
    }
}
